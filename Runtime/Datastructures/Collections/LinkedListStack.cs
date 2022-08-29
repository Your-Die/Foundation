namespace Forgotten
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Chinchillada.Foundation;

    public class LinkedListStack<T> : IStack<T>
    {
        private readonly LinkedList<T> items = new LinkedList<T>();

        public int Count => this.items.Count;

        public void Push(T item)
        {
            this.Remove(item);
            this.items.AddLast(item);
        }

        public T Pop()
        {
            if (this.items.IsEmpty())
                return default;

            var last = this.items.Last();
            this.items.RemoveLast();

            return last;
        }

        public T Peek() => this.items.LastOrDefault();

        public bool Remove(T item) => this.items.Remove(item);

        public IEnumerator<T> GetEnumerator() => this.items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    }
}