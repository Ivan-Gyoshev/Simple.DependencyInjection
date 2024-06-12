using Simple.DI.Tests.Contracts;
using Simple.DI.Tests.Fakes;
using System.Reflection;

namespace Simple.DI.Tests;

public class AttributeTests
{
    [Fact]
    public void TestTransientAttributeUsage()
    {
        IEnumerable<Attribute> atr = typeof(TransientAttributeTarget).GetCustomAttributes();
        ServiceAttribute? target = atr.First() as ServiceAttribute;

        bool hasCorrectValue = target!.Lifetime.Equals(nameof(Lifetime.Transient), StringComparison.Ordinal);

        Assert.True(hasCorrectValue);
    }

    [Fact]
    public void TestScopedAttributeUsage()
    {
        IEnumerable<Attribute> atr = typeof(ScopedAttributeTarget).GetCustomAttributes();
        ServiceAttribute? target = atr.First() as ServiceAttribute;

        bool hasCorrectValue = target!.Lifetime.Equals(nameof(Lifetime.Scoped), StringComparison.Ordinal);

        Assert.True(hasCorrectValue);
    }

    [Fact]
    public void TestSingletonAttributeUsage()
    {
        IEnumerable<Attribute> atr = typeof(SingletonAttributeTarget).GetCustomAttributes();
        ServiceAttribute? target = atr.First() as ServiceAttribute;

        bool hasCorrectValue = target!.Lifetime.Equals(nameof(Lifetime.Singleton), StringComparison.Ordinal);

        Assert.True(hasCorrectValue);
    }

    [Fact]
    public void TestSingletonImplementationAttributeUsage()
    {
        IEnumerable<Attribute> atr = typeof(SingletonAttributeTargetImplementation).GetCustomAttributes();
        ServiceAttribute? target = atr.First() as ServiceAttribute;

        bool hasCorrectLifetime = target!.Lifetime.Equals(nameof(Lifetime.Singleton), StringComparison.Ordinal);
        bool hasCorrectInterface = target!.Interface.Equals(nameof(ISingletonTarget), StringComparison.Ordinal);

        Assert.True(hasCorrectLifetime);
        Assert.True(hasCorrectInterface);
    }

    [Fact]
    public void TestScopedImplementationAttributeUsage()
    {
        IEnumerable<Attribute> atr = typeof(ScopedAttributeTargetImplementation).GetCustomAttributes();
        ServiceAttribute? target = atr.First() as ServiceAttribute;

        bool hasCorrectLifetime = target!.Lifetime.Equals(nameof(Lifetime.Scoped), StringComparison.Ordinal);
        bool hasCorrectInterface = target!.Interface.Equals(nameof(IScopedTarget), StringComparison.Ordinal);

        Assert.True(hasCorrectLifetime);
        Assert.True(hasCorrectInterface);
    }

    [Fact]
    public void TestTransientImplementationAttributeUsage()
    {
        IEnumerable<Attribute> atr = typeof(TransientAttributeTargetImplementation).GetCustomAttributes();
        ServiceAttribute? target = atr.First() as ServiceAttribute;

        bool hasCorrectLifetime = target!.Lifetime.Equals(nameof(Lifetime.Transient), StringComparison.Ordinal);
        bool hasCorrectInterface = target!.Interface.Equals(nameof(ITransientTarget), StringComparison.Ordinal);

        Assert.True(hasCorrectLifetime);
        Assert.True(hasCorrectInterface);
    }

    [Fact]
    public void TestOpenGenericAttributeUsage()
    {
        IEnumerable<Attribute> atr = typeof(Repository<User>).GetCustomAttributes();
        OpenGenericServiceAttribute? target = atr.Where(x => x.GetType().Equals(typeof(OpenGenericServiceAttribute))).Single() as OpenGenericServiceAttribute;

        bool hasCorrectLifetime = target!.Lifetime.Equals(nameof(Lifetime.Singleton), StringComparison.Ordinal);
        bool hasCorrectInterface = target!.Interface.Equals(nameof(IRepository<User>), StringComparison.Ordinal);

        Assert.True(hasCorrectLifetime);
        Assert.True(hasCorrectInterface);
    }
}