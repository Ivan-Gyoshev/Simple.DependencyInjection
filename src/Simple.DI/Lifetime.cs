using Microsoft.Extensions.DependencyInjection;

namespace Simple.DI;

/// <summary>
/// Represents the service lifetime that will be used when registering
/// the class to the IoC Container.
/// </summary>
public readonly record struct Lifetime
{
    /// <summary>
    /// Represents a <see cref="ServiceLifetime.Transient"/> service lifetime.
    /// </summary>
    public const string Transient = nameof(ServiceLifetime.Transient);

    /// <summary>
    /// Represents a <see cref="ServiceLifetime.Scoped"/> service lifetime.
    /// </summary>
    public const string Scoped = nameof(ServiceLifetime.Scoped);

    /// <summary>
    /// Represents a <see cref="ServiceLifetime.Singleton"/> service lifetime.
    /// </summary>
    public const string Singleton = nameof(ServiceLifetime.Singleton);
}