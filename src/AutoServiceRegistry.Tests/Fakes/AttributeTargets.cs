using AutoServiceRegistry.Tests.Contracts;

namespace AutoServiceRegistry.Tests.Fakes;

[ServiceRegistration(Lifetime.Transient)]
public class TransientAttributeTarget
{
}

[ServiceRegistration(Lifetime.Scoped)]
public class ScopedAttributeTarget
{
}

[ServiceRegistration(Lifetime.Singleton)]
public class SingletonAttributeTarget
{
}

[ServiceRegistration(Lifetime.Transient, nameof(ITransientTarget))]
public class TransientAttributeTargetImplementation : ITransientTarget
{
    public bool ReturnTrue()
    {
        return true;
    }
}

[ServiceRegistration(Lifetime.Scoped, nameof(IScopedTarget))]
public class ScopedAttributeTargetImplementation : IScopedTarget
{
    public bool ReturnTrue()
    {
        return true;
    }
}

[ServiceRegistration(Lifetime.Singleton, nameof(ISingletonTarget))]
public class SingletonAttributeTargetImplementation : ISingletonTarget
{
    public bool ReturnTrue()
    {
        return true;
    }
}
