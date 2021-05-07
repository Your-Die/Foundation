namespace Chinchillada
{
    public interface IDelegateContainer<T> : IContainer<T>
    {
        void Refresh();
    }
}