namespace Simple.DependencyInjection.Generator.Models;

/// <summary>
///     Defines different types of registered services.
/// </summary>
internal enum ServiceType
{
    /// <summary>
    /// Represents a regular non-generic service
    /// </summary>
    Regular,

    /// <summary>
    /// Represents an open-generic type of service
    /// </summary>
    OpenGeneric,

    /// <summary>
    /// When the type can not be resolved.
    /// </summary>
    Invalid
    // TODO: Add keyed services
}