namespace AutoServiceRegistry.Generator;

internal sealed record InterfaceData
{
    internal InterfaceData(string name, string containingNamespace)
    {
        Name = name;
        ContainingNamespace = containingNamespace;
    }

    internal string Name { get; private set; }

    internal string ContainingNamespace { get; private set; }

    internal static InterfaceData None = new InterfaceData(string.Empty, string.Empty);

    internal bool IsEmtpy => string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(ContainingNamespace);
}
