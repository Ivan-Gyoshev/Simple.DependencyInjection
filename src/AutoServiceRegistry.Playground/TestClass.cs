namespace AutoServiceRegistry.Playground;

[ServiceRegistration(Lifetime.Transient, nameof(ITestInterface))]
public class TestClass : ITestInterface
{
    public void Print()
    {
        Console.WriteLine("Successfully registered!");
    }
}

[ServiceRegistration(Lifetime.Singleton)]
public class GG
{
    public void Run()
    {

    }
}
