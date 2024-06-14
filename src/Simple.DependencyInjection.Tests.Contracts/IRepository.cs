namespace Simple.DependencyInjection.Tests.Contracts;

public interface IRepository<T>
{
    T Get();
}