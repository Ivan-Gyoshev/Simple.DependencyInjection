namespace AutoServiceRegistry.Generator;

internal sealed record Target
{
    Target() { }

    internal Target(string lifetime, string serviceName, string servicePath, string serviceInterface, string serviceInterfacePath)
    {
        Lifetime = lifetime;
        ServiceName = serviceName;
        ServicePath = servicePath;
        ServiceInterface = serviceInterface;
        ServiceInterfacePath = serviceInterfacePath;
    }

    internal string Lifetime { get; private set; }
    internal string ServiceName { get; private set; }
    internal string ServicePath { get; private set; }
    internal string ServiceInterface { get; private set; }
    internal string ServiceInterfacePath { get; private set; }

    internal static Target Invalid = new Target();
}