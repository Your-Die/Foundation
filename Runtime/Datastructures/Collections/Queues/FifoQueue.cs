namespace Chinchillada
{
    using System.Collections;
    using System.Collections.Generic;

    public class FifoQueue<T> : IQueue<T>
    {
        private readonly Queue<T> queue = new Queue<T>();

        public int Count => this.queue.Count;
        public void Enqueue(T item) => this.queue.Enqueue(item);

        public T Dequeue() => this.queue.Dequeue();

        public T Peek() => this.queue.Peek();
        public void Clear() => this.queue.Clear();

        public IEnumerator<T> GetEnumerator() => this.queue.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}