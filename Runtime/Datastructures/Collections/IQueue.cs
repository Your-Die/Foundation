using System.Collections.Generic;

namespace Chinchillada.Foundation
{
    public interface IQueue<T> : IReadOnlyCollection<T>
    {
        void Enqueue(T item);

        T Dequeue();

        T Peek();
    }
}