using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Utilities
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
        private readonly Queue<T> _queue;
        /// <summary>
        /// The fixed size for this queue.
        /// </summary>
        private int _capacity;
       
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
            get => _capacity;
            set
            {
                //Do nothing if it's the same.
                if (value == _capacity)
                    return;

                //Look if elements need to be dequeued.
                _capacity = value; 
                DequeueExcessValues();
            }
        }

        /// <summary>
        /// Amount of values in this queue.
        /// </summary>
        public int Count => _queue.Count;

        public FixedQueue(int capacity)
        {
            _capacity = capacity;
            _queue = new Queue<T>(capacity + 1);
        }

        /// <summary>
        /// Enqueues the <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to enqueue.</param>
        public void Enqueue(T value)
        {
            _queue.Enqueue(value);
            ValueEnqueued?.Invoke(value);

            DequeueExcessValues();
        }

        /// <summary>
        /// Returns the element at the <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The value at <paramref name="index"/>.</returns>
        public T AtIndex(int index)
        {
            return _queue.ElementAt(index);
        }

        /// <summary>
        /// Clears the queue.
        /// </summary>
        public void Clear()
        {
            _queue.Clear();
        }

        /// <summary>
        /// Dequeues until the queue doesn't exceed capacity anymore.
        /// </summary>
        private void DequeueExcessValues()
        {
            while (_queue.Count > Capacity)
            {
                T value = _queue.Dequeue();
                ExcessDequeued?.Invoke(value);
            }
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            return _queue.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
