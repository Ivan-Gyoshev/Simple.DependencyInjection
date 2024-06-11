namespace Simple.DI;

/// <summary>
///     Specifies how a service would be registered inside the IoC container.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class ServiceAttribute : Attribute
{
    private readonly string _lifetime;
    private readonly string _interface;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ServiceAttribute"/> class.
    /// </summary>
    /// <param name="serviceLifetime">
    ///     Value that indicates the lifetime of the service that is going to be registered.
    /// </param>
    /// <param name="implementationOf">
    ///     Value that points to the interface that the service is implementing
    /// </param>
    public ServiceAttribute(string serviceLifetime, string implementationOf = "")
    {
        _lifetime = serviceLifetime;
        _interface = implementationOf;
    }

    /// <summary>
    ///     Gets the value that indicates the lifetime of the service that is going to be registered.
    /// </summary>
    public string Lifetime { get { return _lifetime; } }

    /// <summary>
    ///     Gets the type of the interface that the service is implementing. (nullable)
    /// </summary>
    public string Interface { get { return _interface; } }
}
