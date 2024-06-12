using Simple.DI.Tests.Contracts;

namespace Simple.DI.Tests.Fakes;

[Service(Lifetime.Transient)]
public class TransientAttributeTarget
{
}

[Service(Lifetime.Scoped)]
public class ScopedAttributeTarget
{
}

[Service(Lifetime.Singleton)]
public class SingletonAttributeTarget
{
}

[Service(Lifetime.Transient, nameof(ITransientTarget))]
public class TransientAttributeTargetImplementation : ITransientTarget
{
    public bool ReturnTrue()
    {
        return true;
    }
}

[Service(Lifetime.Scoped, nameof(IScopedTarget))]
public class ScopedAttributeTargetImplementation : IScopedTarget
{
    public bool ReturnTrue()
    {
        return true;
    }
}

[Service(Lifetime.Singleton, nameof(ISingletonTarget))]
public class SingletonAttributeTargetImplementation : ISingletonTarget
{
    public bool ReturnTrue()
    {
        return true;
    }
}

[OpenGenericService(Lifetime.Singleton, nameof(IRepository<T>))]
public class Repository<T> : IRepository<T>
    where T : class, new()
{
    public T Get() => new T();
}

public class User
{

}
