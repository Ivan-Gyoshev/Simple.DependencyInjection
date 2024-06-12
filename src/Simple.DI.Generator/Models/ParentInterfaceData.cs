namespace Simple.DI.Generator.Models;

/// <summary>
/// Contains interface meta data
/// </summary>
internal sealed record ParentInterfaceData
{
    internal ParentInterfaceData(string name, string containingNamespace)
    {
        Name = name;
        ContainingNamespace = containingNamespace;
    }

    /// <summary>
    /// The name of the interface
    /// </summary>
    internal string Name { get; private set; }

    /// <summary>
    /// The namespace where the interface is located at.
    /// </summary>
    internal string ContainingNamespace { get; private set; }

    internal static ParentInterfaceData None = new ParentInterfaceData(string.Empty, string.Empty);

    internal bool IsEmtpy => string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(ContainingNamespace);
}
