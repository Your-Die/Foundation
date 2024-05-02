namespace Chinchillada
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Last in, first out.
    /// </summary>
    public class LifoQueue<T> : IQueue<T>
    {
        private readonly Stack<T> stack = new Stack<T>();

        public int Count => this.stack.Count;

        public void Enqueue(T item) => this.stack.Push(item);

        public T Dequeue() => this.stack.Pop();

        public T Peek() => this.stack.Peek();
        public void Clear() => this.stack.Clear();

        public IEnumerator<T> GetEnumerator() => this.stack.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}