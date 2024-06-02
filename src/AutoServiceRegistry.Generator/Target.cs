namespace AutoServiceRegistry.Generator;

internal sealed record Target
{
    Target() { }

    internal Target(string lifetime, string serviceName, string servicePath, string serviceInterface, string serviceInterfacePath)
    {
        Lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));
        ServiceName = serviceName ?? throw new ArgumentNullException(nameof(serviceName)); ;
        ServicePath = servicePath ?? throw new ArgumentNullException(nameof(servicePath)); ;
        ServiceInterface = serviceInterface;
        ServiceInterfacePath = serviceInterfacePath;
    }

    /// <summary>
    /// The lifetime of the target class.
    /// </summary>
    internal string Lifetime { get; private set; }

    /// <summary>
    /// The name of the class that is being registered.
    /// </summary>
    internal string ServiceName { get; private set; }

    /// <summary>
    /// The full namespace of the class that is being registered.
    /// </summary>
    internal string ServicePath { get; private set; }

    /// <summary>
    /// The name of the interface that the <see cref="ServiceName"/> implements. May be null if there is none.
    /// </summary>
    internal string ServiceInterface { get; private set; }

    /// <summary>
    /// The full namespace of the interface that the <see cref="ServiceName"/> implements. May be null if there is none.
    /// </summary>
    internal string ServiceInterfacePath { get; private set; }

    internal static Target Invalid = new Target();

    internal bool IsValid => string.IsNullOrEmpty(Lifetime) is false
        && string.IsNullOrEmpty(ServiceName) is false
        && string.IsNullOrEmpty(ServicePath) is false;
}
