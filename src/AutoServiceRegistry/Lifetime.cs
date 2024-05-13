using Microsoft.Extensions.DependencyInjection;

namespace AutoServiceRegistry;

/// <summary>
/// Represents the service lifetime that will be used when adding
/// the service to the IoC Container
/// </summary>
public readonly record struct Lifetime
{
    /// <summary>
    /// Represents a Transient service lifetime.
    /// </summary>
    public const string Transient = nameof(ServiceLifetime.Transient);

    /// <summary>
    /// Represents a Scoped service lifetime.
    /// </summary>
    public const string Scoped = nameof(ServiceLifetime.Scoped);

    /// <summary>
    /// Represents a Singleton service lifetime.
    /// </summary>
    public const string Singleton = nameof(ServiceLifetime.Singleton);
}
