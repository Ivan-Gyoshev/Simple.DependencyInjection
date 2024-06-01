using AutoServiceRegistry.Tests.Contracts;
using AutoServiceRegistry.Tests.Fakes;
using System.Reflection;

namespace AutoServiceRegistry.Tests;

public class AttributeTests
{
    [Fact]
    public void TestTransientAttributeUsage()
    {
        IEnumerable<Attribute> atr = typeof(TransientAttributeTarget).GetCustomAttributes();
        ServiceRegistrationAttribute? target = atr.First() as ServiceRegistrationAttribute;

        bool hasCorrectValue = target!.ServiceLifetime.Equals(nameof(Lifetime.Transient), StringComparison.Ordinal);

        Assert.True(hasCorrectValue);
    }

    [Fact]
    public void TestScopedAttributeUsage()
    {
        IEnumerable<Attribute> atr = typeof(ScopedAttributeTarget).GetCustomAttributes();
        ServiceRegistrationAttribute? target = atr.First() as ServiceRegistrationAttribute;

        bool hasCorrectValue = target!.ServiceLifetime.Equals(nameof(Lifetime.Scoped), StringComparison.Ordinal);

        Assert.True(hasCorrectValue);
    }

    [Fact]
    public void TestSingletonAttributeUsage()
    {
        IEnumerable<Attribute> atr = typeof(SingletonAttributeTarget).GetCustomAttributes();
        ServiceRegistrationAttribute? target = atr.First() as ServiceRegistrationAttribute;

        bool hasCorrectValue = target!.ServiceLifetime.Equals(nameof(Lifetime.Singleton), StringComparison.Ordinal);

        Assert.True(hasCorrectValue);
    }

    [Fact]
    public void TestSingletonImplementationAttributeUsage()
    {
        IEnumerable<Attribute> atr = typeof(SingletonAttributeTargetImplementation).GetCustomAttributes();
        ServiceRegistrationAttribute? target = atr.First() as ServiceRegistrationAttribute;

        bool hasCorrectLifetime = target!.ServiceLifetime.Equals(nameof(Lifetime.Singleton), StringComparison.Ordinal);
        bool hasCorrectImplementation = target!.ServiceInterface.Equals(nameof(ISingletonTarget), StringComparison.Ordinal);

        Assert.True(hasCorrectLifetime);
        Assert.True(hasCorrectImplementation);
    }

    [Fact]
    public void TestScopedImplementationAttributeUsage()
    {
        IEnumerable<Attribute> atr = typeof(ScopedAttributeTargetImplementation).GetCustomAttributes();
        ServiceRegistrationAttribute? target = atr.First() as ServiceRegistrationAttribute;

        bool hasCorrectLifetime = target!.ServiceLifetime.Equals(nameof(Lifetime.Scoped), StringComparison.Ordinal);
        bool hasCorrectImplementation = target!.ServiceInterface.Equals(nameof(IScopedTarget), StringComparison.Ordinal);

        Assert.True(hasCorrectLifetime);
        Assert.True(hasCorrectImplementation);
    }

    [Fact]
    public void TestTransientImplementationAttributeUsage()
    {
        IEnumerable<Attribute> atr = typeof(TransientAttributeTargetImplementation).GetCustomAttributes();
        ServiceRegistrationAttribute? target = atr.First() as ServiceRegistrationAttribute;

        bool hasCorrectLifetime = target!.ServiceLifetime.Equals(nameof(Lifetime.Transient), StringComparison.Ordinal);
        bool hasCorrectImplementation = target!.ServiceInterface.Equals(nameof(ITransientTarget), StringComparison.Ordinal);

        Assert.True(hasCorrectLifetime);
        Assert.True(hasCorrectImplementation);
    }
}