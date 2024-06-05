using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace AutoServiceRegistry.Generator;

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
    internal static InterfaceData FindInterfaceData(this GeneratorSyntaxContext generatorContext, TypedConstant serviceInterface)
    {
        if (string.IsNullOrEmpty((string)serviceInterface.Value) is false)
        {
            string interfaceName = (string)serviceInterface.Value;

            INamespaceSymbol globalNamespace = generatorContext.SemanticModel.Compilation.GlobalNamespace;
            INamedTypeSymbol namespaceLocation = globalNamespace.ScanGlobalNamespaceFor(interfaceName);

            return new InterfaceData(interfaceName, namespaceLocation.ContainingNamespace.ToDisplayString());
        }

        return InterfaceData.None;
    }

    /// <summary>
    /// Builds the string for the generator that is going to be used as the <b>ServiceDescriptor</b> of the registration.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    internal static string TransformToServiceDescriptor(this Target target)
    {
        string s_interface = string.IsNullOrEmpty(target.ServiceInterface)
            ? target.ServiceName
            : target.ServiceInterface;

        string s_interfacePath = string.IsNullOrEmpty(target.ServiceInterfacePath)
            ? target.ServicePath
            : target.ServiceInterfacePath;

        return $"\t\t\tservices.Add(new ServiceDescriptor(typeof({s_interfacePath}.{s_interface}), typeof({target.ServicePath}.{target.ServiceName}), ServiceLifetime.{target.Lifetime}));";
    }

    /// <summary>
    /// Recursively searches inside each namespace symbol until it finds the match.
    /// </summary>
    /// <param name="namespaceSymbol"></param>
    /// <param name="interfaceName"></param>
    /// <returns> 
    /// If found, returns <see cref="InterfaceData"/>.
    /// If none are found, returns <see cref="InterfaceData.None"/>
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