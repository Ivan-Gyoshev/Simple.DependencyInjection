﻿using Microsoft.Extensions.DependencyInjection;
using AutoServiceRegistry.Generator;
using AutoServiceRegistry.Tests.Fakes;

namespace AutoServiceRegistry.Tests;

public class RegistryTests
{
    [Fact]
    public void ShouldContainRegisteredService()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddRegistry();

        DefaultServiceProviderFactory serviceProviderFactory = new DefaultServiceProviderFactory();
        IServiceCollection builder = serviceProviderFactory.CreateBuilder(services);
        ServiceProvider serviceProvider = builder.BuildServiceProvider();

        var resolved = serviceProvider.GetRequiredService<ScopedAttributeTarget>();

        Assert.NotNull(resolved);
    }
}