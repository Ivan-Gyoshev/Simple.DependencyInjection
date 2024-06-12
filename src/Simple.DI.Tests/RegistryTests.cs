using Microsoft.Extensions.DependencyInjection;
using Simple.DI.Tests.Contracts;
using Simple.DI.Tests.Fakes;

namespace Simple.DI.Tests;

public class RegistryTests
{
    [Fact]
    public void ShouldContainRegisteredService()
    {
        IServiceCollection services = new ServiceCollection();
        services.RegisterResolvableServices();

        DefaultServiceProviderFactory serviceProviderFactory = new DefaultServiceProviderFactory();
        IServiceCollection builder = serviceProviderFactory.CreateBuilder(services);
        ServiceProvider serviceProvider = builder.BuildServiceProvider();

        var resolved = serviceProvider.GetRequiredService<ScopedAttributeTarget>();

        Assert.NotNull(resolved);
    }

    [Fact]
    public void ShouldContainOpenGenericService()
    {
        IServiceCollection services = new ServiceCollection();
        services.RegisterResolvableServices();

        DefaultServiceProviderFactory serviceProviderFactory = new DefaultServiceProviderFactory();
        IServiceCollection builder = serviceProviderFactory.CreateBuilder(services);
        ServiceProvider serviceProvider = builder.BuildServiceProvider();

        var resolved = serviceProvider.GetRequiredService<IRepository<User>>();
        Assert.NotNull(resolved);
    }
}