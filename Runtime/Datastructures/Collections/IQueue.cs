using System.Collections.Generic;

namespace Chinchillada
{
    public interface IQueue<T> : IReadOnlyCollection<T>
    {
        void Enqueue(T item);

        T Dequeue();

        T Peek();
    }
}