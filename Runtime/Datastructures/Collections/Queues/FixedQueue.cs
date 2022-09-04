using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada
{
    /// <summary>
    /// Wrapper class for Queue which stays at a fixed capacity, and automatically dequeues values when the capacity is exceeded.
    /// </summary>
    /// <typeparam name="T">The type of the values in the queue.</typeparam>
    public class FixedQueue<T> : IEnumerable<T>
    {
        /// <summary>
        /// The queue instance that this class wraps.
        /// </summary>
        private readonly LinkedList<T> queue;
        /// <summary>
        /// The fixed size for this queue.
        /// </summary>
        private int capacity;

        /// <summary>
        /// Event that is called when an excess element is dequeued.
        /// </summary>
        public event Action<T> ExcessDequeued;

        /// <summary>
        /// Event this is called when a element is enqueued.
        /// </summary>
        public event Action<T> ValueEnqueued;

        /// <summary>
        /// The capacity that the queue may not exceed.
        /// </summary>
        public int Capacity
        {
            get => this.capacity;
            set
            {
                //Do nothing if it's the same.
                if (value == this.capacity)
                    return;

                //Look if elements need to be dequeued.
                this.capacity = value;
                this.DequeueExcessValues();
            }
        }

        /// <summary>
        /// Amount of values in this queue.
        /// </summary>
        public int Count => this.queue.Count;

        public FixedQueue(int capacity)
        {
            this.capacity = capacity;
            this.queue = new LinkedList<T>();
        }

        /// <summary>
        /// Enqueues the <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to enqueue.</param>
        public void Enqueue(T value)
        {
            this.queue.AddLast(value);
            this.ValueEnqueued?.Invoke(value);

            this.DequeueExcessValues();
        }

        public void Remove(T value)
        {
            this.queue.Remove(value);
        }

        /// <summary>
        /// Returns the element at the <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The value at <paramref name="index"/>.</returns>
        public T AtIndex(int index)
        {
            return this.queue.ElementAt(index);
        }

        /// <summary>
        /// Clears the queue.
        /// </summary>
        public void Clear()
        {
            this.queue.Clear();
        }
        public IEnumerable<T> RemoveAll(Func<T, bool> predicate) => this.queue.RemoveAll(predicate);

        public bool Contains(T value) => this.queue.Contains(value);

        /// <summary>
        /// Dequeues until the queue doesn't exceed capacity anymore.
        /// </summary>
        private void DequeueExcessValues()
        {
            while (this.queue.Count > this.Capacity)
            {
                T value = this.queue.First.Value;
                this.queue.RemoveFirst();

                this.ExcessDequeued?.Invoke(value);
            }
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            return this.queue.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
