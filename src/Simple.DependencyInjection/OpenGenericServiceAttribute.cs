namespace Simple.DependencyInjection;

/// <summary>
///     Specifies how an open generic service would be registered inside the IoC container.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class OpenGenericServiceAttribute : Attribute
{
    private readonly string _lifetime;
    private readonly string _interface;

    /// <summary>
    ///     Initializes a new instance of the <see cref="OpenGenericServiceAttribute"/> class.
    /// </summary>
    /// <param name="serviceLifetime">
    ///     Value that indicates the lifetime of the service that is going to be registered.
    /// </param>
    /// <param name="implementationOf">
    ///     Value that points to the interface that the service is implementing
    /// </param>
    public OpenGenericServiceAttribute(string serviceLifetime, string implementationOf = "")
    {
        _lifetime = serviceLifetime;
        _interface = implementationOf;
    }

    /// <summary>
    ///     Gets the value that indicates the lifetime of the service that is going to be registered.
    /// </summary>
    public string Lifetime { get => _lifetime; }

    /// <summary>
    ///     Gets the type of the interface that the service is implementing. (nullable)
    /// </summary>
    public string Interface { get => _interface; }
}
