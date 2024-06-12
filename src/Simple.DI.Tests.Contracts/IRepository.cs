namespace Simple.DI.Tests.Contracts;

public interface IRepository<T>
{
    T Get();
}