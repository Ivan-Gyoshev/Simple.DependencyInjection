using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using Simple.DependencyInjection.Generator.Models;

namespace Simple.DependencyInjection.Generator;

/// <summary>
/// Extensions methods used inside the <see cref="ServiceRegistryGenerator"/>.
/// </summary>
internal static class GeneratorExtensions
{
    /// <summary>
    /// Points whether the target of the generator is class.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    internal static bool IsClassDeclaration(this SyntaxNode node)
    {
        return node is ClassDeclarationSyntax;
    }

    /// <summary>
    /// Finds the interface inside the Global Namespace of the <see cref="GeneratorSyntaxContext"/>.
    /// </summary>
    /// <param name="generatorContext"></param>
    /// <param name="serviceInterface"></param>
    /// <returns></returns>
    internal static ParentInterfaceData FindInterfaceData(this GeneratorSyntaxContext generatorContext, TypedConstant serviceInterface)
    {
        string interfaceName = (string)serviceInterface.Value;

        if (string.IsNullOrEmpty(interfaceName) is false)
        {
            INamespaceSymbol globalNamespace = generatorContext.SemanticModel.Compilation.GlobalNamespace;
            INamedTypeSymbol namespaceLocation = globalNamespace.ScanGlobalNamespaceFor(interfaceName);

            return new ParentInterfaceData(interfaceName, namespaceLocation.ContainingNamespace.ToDisplayString());
        }

        return ParentInterfaceData.None;
    }

    /// <summary>
    /// Builds the string for the generator that is going to be used as the <b>ServiceDescriptor</b> of the registration.
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    internal static string TransformToServiceDescriptor(this Service service)
    {
        string serviceType = string.IsNullOrEmpty(service.ParentInterface)
            ? service.Name
            : service.ParentInterface;

        string serviceTypePath = string.IsNullOrEmpty(service.ParentInterfacePath)
            ? service.Path
            : service.ParentInterfacePath;


        return service.Type switch
        {
            ServiceType.Regular => $"\t\t\tservices.Add(new ServiceDescriptor(typeof({serviceTypePath}.{serviceType}), typeof({service.Path}.{service.Name}), ServiceLifetime.{service.Lifetime}));",
            ServiceType.OpenGeneric => $"\t\t\tservices.Add(new ServiceDescriptor(typeof({serviceTypePath}.{serviceType}<>), typeof({service.Path}.{service.Name}<>), ServiceLifetime.{service.Lifetime}));",
            _ => string.Empty
        };
    }

    /// <summary>
    /// Recursively searches inside each namespace symbol until it finds the match.
    /// </summary>
    /// <param name="namespaceSymbol"></param>
    /// <param name="interfaceName"></param>
    /// <returns> 
    /// If found, returns <see cref="ParentInterfaceData"/>.
    /// If none are found, returns <see cref="ParentInterfaceData.None"/>
    /// </returns>
    private static INamedTypeSymbol ScanGlobalNamespaceFor(this INamespaceSymbol namespaceSymbol, string interfaceName)
    {
        foreach (var member in namespaceSymbol.GetMembers())
        {
            if (member is INamespaceSymbol nestedNamespace)
            {
                INamedTypeSymbol result = nestedNamespace.ScanGlobalNamespaceFor(interfaceName);
                if (result is not null)
                {
                    return result;
                }
            }
            else if (member is INamedTypeSymbol typeSymbol && typeSymbol.TypeKind == TypeKind.Interface && typeSymbol.Name == interfaceName)
            {
                return typeSymbol;
            }
        }

        return default;
    }
}