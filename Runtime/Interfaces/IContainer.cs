namespace Chinchillada.Foundation
{
    public interface IContainer<T> : ISource<T>
    {
        void Set(T value);
    }
}