namespace Simple.DependencyInjection.Generator.Models;

/// <summary>
/// Registry target
/// </summary>
internal sealed record Service
{
    Service() { }

    internal Service(string lifetime, string name, string path, string parentInterface, string parentInterfacePath, ServiceType type)
    {
        Lifetime = string.IsNullOrEmpty(lifetime) ? throw new ArgumentNullException(nameof(lifetime)) : lifetime;
        Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
        Path = string.IsNullOrEmpty(path) ? throw new ArgumentNullException(nameof(path)) : path;
        ParentInterface = parentInterface;
        ParentInterfacePath = parentInterfacePath;
        Type = type;
    }

    /// <summary>
    /// The lifetime of the target class.
    /// </summary>
    internal string Lifetime { get; private set; }

    /// <summary>
    /// The name of the class that is being registered.
    /// </summary>
    internal string Name { get; private set; }

    /// <summary>
    /// The full namespace of the class that is being registered.
    /// </summary>
    internal string Path { get; private set; }

    /// <summary>
    /// The name of the interface that the <see cref="Name"/> implements. May be null if there is none.
    /// </summary>
    internal string ParentInterface { get; private set; }

    /// <summary>
    /// The full namespace of the interface that the <see cref="Name"/> implements. May be null if there is none.
    /// </summary>
    internal string ParentInterfacePath { get; private set; }

    /// <summary>
    /// Represents the type of the service that is being registered.
    /// </summary>
    internal ServiceType Type { get; private set; }

    internal static Service Invalid = new Service();

    internal bool IsValid => string.IsNullOrEmpty(Lifetime) is false
        && string.IsNullOrEmpty(Name) is false
        && string.IsNullOrEmpty(Path) is false;
}
