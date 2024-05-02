using System;
using System.Collections;
using System.Collections.Generic;

namespace Chinchillada
{
    public class PriorityQueueAdapter<T, TPriority> : IQueue<T> where TPriority : IComparable<TPriority>
    {
        private readonly IPriorityQueue<T, TPriority> queue;
        private readonly Func<T, TPriority> priorityFunction;

        public int Count => this.queue.Count;

        public PriorityQueueAdapter(IPriorityQueue<T, TPriority> queue, Func<T, TPriority> priorityFunction)
        {
            this.queue = queue;
            this.priorityFunction = priorityFunction;
        }

        public void Enqueue(T item)
        {
            var priority = this.priorityFunction.Invoke(item);
            this.queue.Add(item, priority);
        }

        public T Dequeue() => this.queue.Pop();

        public T Peek() => this.queue.Peek();

        public void Clear() => this.queue.Clear();

        public IEnumerator<T> GetEnumerator() => this.queue.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}