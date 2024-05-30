﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Text;

namespace AutoServiceRegistry.Generator;

[Generator]
public sealed class ServiceRegistryGenerator : IIncrementalGenerator
{
    private const string _attributeName = "ServiceRegistrationAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<Target> services = context.SyntaxProvider.CreateSyntaxProvider
           (
            predicate: static (syntaxNode, _) => IsTarget(syntaxNode),
            transform: (generatorContext, cn) => GetTarget(generatorContext, cn)
           ).Where(static target => string.IsNullOrEmpty(target.Lifetime) is false
                                 && string.IsNullOrEmpty(target.ServiceName) is false);

        context.RegisterSourceOutput(services.Collect(), Execute);
    }

    private void Execute(SourceProductionContext context, ImmutableArray<Target> args)
    {
        try
        {
            context.CancellationToken.ThrowIfCancellationRequested();

            StringBuilder registryBuilder = new();

            registryBuilder.Append($@"// <auto-generated/>
// IMPORTANT: This class is auto-generated

using Microsoft.Extensions.DependencyInjection;

namespace AutoServiceRegistry
{{
    public static class ServiceRegistrator
    {{
        public static IServiceCollection AddRegistry(this IServiceCollection services)
        {{
");
            foreach (Target target in args)
            {
                registryBuilder.AppendLine(BuildServiceDescriptor(target));
            }

            registryBuilder.Append(@$"
            return services;
        }}
    }}
}}");

            string registry = registryBuilder.ToString();
            context.AddSource("AutoServiceRegistry.g.cs", SourceText.From(registry, Encoding.UTF8));
        }
        catch (Exception ex)
        {
            ILogger logger = new DiagnosticLogger(context);
            logger.LogError($"Message: {ex.Message}, Trace: {ex.StackTrace}");
        }
    }

    private static bool IsTarget(SyntaxNode node) => node is ClassDeclarationSyntax;

    private static Target GetTarget(GeneratorSyntaxContext generatorContext, CancellationToken token)
    {
        try
        {
            ClassDeclarationSyntax syntax = (ClassDeclarationSyntax)generatorContext.Node;
            ISymbol symbol = generatorContext.SemanticModel.GetDeclaredSymbol(syntax, token);

            if (symbol.IsAbstract is false)
            {
                AttributeData attribute = symbol.GetAttributes().Where(atr => atr.AttributeClass.Name.Equals(_attributeName)).SingleOrDefault();

                if (attribute is not null)
                {
                    ImmutableArray<TypedConstant> attributeValues = attribute.ConstructorArguments;
                    TypedConstant lifetime = attributeValues[0];
                    TypedConstant serviceInterface = attributeValues[1];

                    string interfaceName, interfacePath;
                    TryFindInterfaceData(generatorContext, serviceInterface, out interfaceName, out interfacePath);

                    return new Target((string)lifetime.Value, symbol.Name, symbol.ContainingNamespace.ToDisplayString(), interfaceName, interfacePath);
                }
            }
            return Target.Invalid;

        }
        catch (Exception)
        {
            return Target.Invalid;
        }
    }

    private static void TryFindInterfaceData(GeneratorSyntaxContext generatorContext, TypedConstant serviceInterface, out string interfaceName, out string interfacePath)
    {
        interfaceName = string.Empty;
        interfacePath = string.Empty;

        if (string.IsNullOrEmpty((string)serviceInterface.Value) is false)
        {
            string i_name = (string)serviceInterface.Value;

            IEnumerable<InterfaceDeclarationSyntax> ifaces = generatorContext.SemanticModel.Compilation.SyntaxTrees.SelectMany(x => x.GetRoot().DescendantNodes().OfType<InterfaceDeclarationSyntax>());
            InterfaceDeclarationSyntax match = ifaces.Where(x => x.Identifier.Text.Equals(i_name)).FirstOrDefault();

            if (match is not null)
            {
                NamespaceDeclarationSyntax namespaceSyntax = match.Ancestors().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();

                if (namespaceSyntax is not null)
                {
                    interfacePath = namespaceSyntax.Name.ToString();
                }
                else
                {
                    FileScopedNamespaceDeclarationSyntax filedNamespaceSyntax = match.Ancestors().OfType<FileScopedNamespaceDeclarationSyntax>().FirstOrDefault();
                    if (filedNamespaceSyntax is not null)
                        interfacePath = filedNamespaceSyntax.Name.ToString();
                }
            }

            interfaceName = i_name;
        }
    }

    private static string BuildServiceDescriptor(Target target)
    {
        string s_interface = string.IsNullOrEmpty(target.ServiceInterface) ? target.ServiceName : target.ServiceInterface;
        string s_interfacePath = string.IsNullOrEmpty(target.ServiceInterfacePath) ? target.ServicePath : target.ServiceInterfacePath;

        return $"\t\t\tservices.Add(new ServiceDescriptor(typeof({s_interfacePath}.{s_interface}), typeof({target.ServicePath}.{target.ServiceName}), ServiceLifetime.{target.Lifetime}));";
    }
}
