namespace Chinchillada
{
    public interface IContainer<T> : ISource<T>
    {
        void Set(T value);
    }
}