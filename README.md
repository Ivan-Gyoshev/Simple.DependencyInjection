# AutoServiceRegistry

AutoServiceRegistry is tool powered by the dotnet [Source Generator](doc:https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/source-generators-overview), designed to automate the registration of services into the Inversion of Control (IoC) container in .NET applications. This tool significantly simplifies dependency injection setup, reducing boilerplate code and enhancing maintainability and readability.

Useful part of this tool is that you can easily understand how each service in your application is registered by just looking at the attribute of the given service.

The registration code is not hidden, so you don't need to worry how it is happening. It is just auto-generated and placed in appropriate file which you can access anytime.

## Features
- <b>Automatic Service Registration</b>: Automatically scans for service classes and interfaces, registering them inside the IoC container.

- <b>Customizable Scoping</b>: Allows configuration of service lifetimes (transient, scoped, singleton) through attributes or conventions.

- <b>Performance Optimization</b>: Leverages source generation for compile-time registration, ensuring minimal runtime overhead.

## Usage
### Installation

Add the AutoServiceRegistry package to your project using the .NET CLI:

``` sh
dotnet add package AutoServiceRegistry
```
Or via the NuGet Package Manager in Visual Studio.

![Nuget-Image](/assets/preview.png)

### Code example
For the code example we are going to use a simple Worker Service.

1. To use the automatic service registration, you should invoke the `.UseAutoServiceRegistration()` extension method to your <b>IServiceCollection</b>.

    #### Program.cs
    ``` csharp
    using AutoServiceRegistry;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    namespace Demo;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
            builder.Services.UseAutoServiceRegistration();   // <----------

            var host = builder.Build();
            host.Run();
        }
    }
    ```

2. Applying the attributes to your services

-   Service that does not implement any interface
    ``` csharp
    [ServiceRegistration(Lifetime.Transient)]
    public class TransientAttributeTarget
    {
    }
    ```
- Service that implements an interface
    ``` csharp
    [ServiceRegistration(Lifetime.Transient, nameof(ITransientTarget))]
    public class TransientAttributeTargetImplementation : ITransientTarget
    {
        public bool ReturnTrue()
        {
            return true;
        }
    }
    ```

#### Lifetime object
Follows the <b>ServiceLifetime</b> enum that comes from the [Microsoft DI package](doc:https://github.com/dotnet/runtime/blob/main/src/libraries/Microsoft.Extensions.DependencyInjection/README.md)

- Lifetime.Transient
- Lifetime.Scoped
- Lifetime.Singleton

## License


## Afterword 
By automating service registration, <b>AutoServiceRegistry</b> streamlines the development process, ensuring clean, and readable code. Embrace the power of source generators and simplify your dependency injection setup.