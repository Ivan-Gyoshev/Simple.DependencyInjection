using AutoServiceRegistry.Generator;

namespace AutoServiceRegistry.Playground
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
            builder.Services.AddRegistry();

            var host = builder.Build();
            host.Run();
        }
    }
}