using System.ComponentModel.DataAnnotations;

namespace AutoServiceRegistry;

/// <summary>
///     Specifies how a service would be registered inside the IoC container.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class ServiceRegistrationAttribute : Attribute
{
    private readonly string _serviceLifetime;
    private readonly string _serviceInterface;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ServiceRegistrationAttribute"/> class.
    /// </summary>
    /// <param name="serviceLifetime">
    ///     Value that indicates the lifetime of the service that is going to be registered.
    /// </param>
    /// <param name="implementationOf">
    ///     Value that points to the interface that the service is implementing
    /// </param>
    public ServiceRegistrationAttribute(string serviceLifetime, string implementationOf = "")
    {
        _serviceLifetime = serviceLifetime;
        _serviceInterface = implementationOf;
    }

    /// <summary>
    ///     Gets the value that indicates the lifetime of the service that is going to be registered.
    /// </summary>
    public string ServiceLifetime { get { return _serviceLifetime; } }

    /// <summary>
    ///     Gets the type of the interface that the service is implementing. (nullable)
    /// </summary>
    public string ServiceInterface { get { return _serviceInterface; } }
}
