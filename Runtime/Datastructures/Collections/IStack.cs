namespace Forgotten
{
    using System.Collections.Generic;

    public interface IStack<T> : IReadOnlyCollection<T>
    {
        void Push(T item);
        T Pop();
        T Peek();
    }
}