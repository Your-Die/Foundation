using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Wrapper class for <see cref="IEnumerable{T}"/> that enumerates into a list so multiple enumerations are supported.
    /// Additional <see cref="IEnumerable{T}"/> can be added as range and will be enumerated in a FIFO manner.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LazyList<T> : IList<T>
    {
        /// <summary>
        /// The queue of <see cref="IEnumerable{T}"/> that are to be enumerated.
        /// </summary>
        private readonly Queue<IEnumerable<T>> enumerables = new Queue<IEnumerable<T>>();

        /// <summary>
        /// The list that we enumerate the results of the <see cref="enumerables"/> into.
        /// </summary>
        private readonly List<T> list = new List<T>();

        /// <summary>
        /// The enumerable that we are currently enumerating.
        /// </summary>
        private IEnumerator<T> enumerator;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <summary>
        /// Returns the amount of elements in this <see cref="IList"/>.
        /// </summary>
        /// <remarks>This will enumerate the wrapped <see cref="IEnumerable{T}"/> fully.</remarks>
        public int Count
        {
            get
            {
                this.EnumerateFull();
                return this.list.Count;
            }
        }

        /// <summary>
        /// Construct a new <see cref="LazyList{T}"/> around the <paramref name="enumerable"/>.
        /// </summary>
        /// <param name="enumerable">The enumerable we want to wrap in <see cref="LazyList{T}"/>.</param>
        public LazyList(IEnumerable<T> enumerable) => this.enumerables.Enqueue(enumerable);

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var p in this.EnumerateList())
                yield return p;

            foreach (var p1 in this.EnumerateCurrent())
                yield return p1;

            foreach (var p2 in this.EnumerateQueue())
                yield return p2;
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        /// Add the <paramref name="item"/> to the end of the list.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(T item)
        {
            // If we have no enumerables in queue, we can add it directly to the list.
            if (this.enumerator == null && this.enumerables.IsEmpty())
            {
                this.list.Add(item);
            }
            else
            {
                // Wrap it in an enumerable so we can add it to the queue.
                var enumerable = Enumerables.Single(item);
                this.enumerables.Enqueue(enumerable);
            }
        }

        /// <summary>
        /// Adds the <paramref name="range"/> to the end of the list.
        /// </summary>
        /// <param name="range">The range to add.</param>
        public void AddRange(IEnumerable<T> range) => this.enumerables.Enqueue(range);

        /// <summary>
        /// Clears the lazy list.
        /// </summary>
        public void Clear()
        {
            this.list.Clear();
            this.enumerator = null;
            this.enumerables.Clear();
        }

        /// <summary>
        /// Checks if the <paramref name="item"/> is contained in this <see cref="IList{T}"/>.
        /// </summary>
        /// <param name="item">The item we want to find.</param>
        /// <returns>True if the item is in the list, false if not.</returns>
        public bool Contains(T item) => this.IndexOf(item) > 0;

        /// <summary>
        /// Copies the contents of this <see cref="IList{T}"/> into the <paramref name="array"/>.
        /// </summary>
        /// <param name="array">The array to copy the contents of this <see cref="LazyList{T}"/> into.</param>
        /// <param name="arrayIndex">The index of the <paramref name="array"/> we want to start copying to.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (T element in this)
            {
                array[arrayIndex++] = element;
            }
        }

        /// <summary>
        /// Removes the first instance of the <paramref name="item"/> from this <see cref="IList{T}"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            // Find the item.
            int index = this.IndexOf(item);

            // Item not found.
            if (index < 0)
                return false;

            // Remove the item.
            this.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Finds the <paramref name="item"/> in this <see cref="LazyList{T}"/> and returns the index.
        /// </summary>
        /// <param name="item">The item we want to find.</param>
        /// <returns>The index of the item if it is contained in this <see cref="LazyList{T}"/> otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            // Define local function for checking equality.
            var comparer = Comparer<T>.Default;
            bool IsSame(T element) => comparer.Compare(item, element) == 0;

            // Find the index.
            int index = 0;
            foreach (T element in this)
            {
                // Item found.
                if (IsSame(element))
                {
                    return index;
                }

                index++;
            }

            // Item not found.
            return -1;
        }

        /// <summary>
        /// Insert the <paramref name="item"/> at the <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index we want to insert the item at.</param>
        /// <param name="item">The item we want to insert.</param>
        public void Insert(int index, T item)
        {
            this.EnumerateUntil(index);
            this.list.Insert(index, item);
        }

        /// <summary>
        /// Removes the item at the <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index of the item we want to remove.</param>
        public void RemoveAt(int index)
        {
            this.EnumerateUntil(index);
            this.list.RemoveAt(index);
        }

        /// <summary>
        /// Gets or sets the item at the <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The item at <see cref="index"/>.</returns>
        public T this[int index]
        {
            get
            {
                this.EnumerateUntil(index);
                return this.list[index];
            }
            set
            {
                this.EnumerateUntil(index);
                this.list[index] = value;
            }
        }

        /// <summary>
        /// Fully enumerates the <see cref="IEnumerable{T}"/> wrapped in this <see cref="LazyList{T}"/>
        /// into the list.
        /// </summary>
        public void EnumerateFull()
        {
            this.EnumerateUntil(int.MaxValue);
        }
        
        /// <summary>
        /// Enumerates all <see cref="IEnumerable{T}"/> in the <see cref="enumerables"/> queue.
        /// </summary>
        /// <returns>The enumeration of the queue.</returns>
        private IEnumerable<T> EnumerateQueue()
        {
            // Go to the queued enumerables.
            while (this.enumerables.Any())
            {
                // Get the next enumerable.
                var enumerable = this.enumerables.Dequeue();
                this.enumerator = enumerable.GetEnumerator();

                // Enumerate.
                do
                {
                    yield return this.enumerator.Current;
                } while (this.enumerator.MoveNext());
            }

            this.enumerator = null;
        }

        /// <summary>
        /// Enumerate the current <see cref="enumerator"/>.
        /// If we exited enumeration previously without fully enumerating the enumerable,
        /// we keep reference of the enumerator in <see cref="enumerator"/>.
        /// this method completes the enumeration on it.
        /// </summary>
        /// <returns>The enumeration.</returns>
        private IEnumerable<T> EnumerateCurrent()
        {
            if (this.enumerator == null)
                yield break;

            // go through the current enumerator. 
            // We start with a MoveNext() here because if _enumerator is assigned,
            // It previously exited enumeration after an evaluation of the Current without 
            // calling move next.
            while (this.enumerator.MoveNext())
            {
                // Get element and add it to list.
                T element = this.enumerator.Current;
                this.list.Add(element);

                yield return element;
            }

            this.enumerator = null;
        }

        /// <summary>
        /// Enumerates through the <see cref="list"/>.
        /// </summary>
        /// <returns>The enumeration of the <see cref="list"/>.</returns>
        private IEnumerable<T> EnumerateList()
        {
            // First loop through the elements we already enumerated.
            foreach (T element in this.list)
                yield return element;
        }

        /// <summary>
        /// Enumerates until the <paramref name="index"/>.
        /// </summary>
        /// <remarks>
        /// This is used as a helper method to be able to do indexing operations,
        /// ensuring the index is in the list if we have enough elements in this <see cref="LazyList{T}"/>
        /// </remarks>
        /// <param name="index">The index we want to enumerate to.</param>
        private void EnumerateUntil(int index)
        {
            // Account of elements in the list.
            index -= this.list.Count;
            if (index <= 0)
                return;

            // Enumerate over current.
            foreach (T unused in this.EnumerateCurrent())
            {
                index--;
                if (index <= 0)
                    return;
            }

            // Enumerate over the enumerables in the queue.
            foreach (T unused in this.EnumerateQueue())
            {
                index--;
                if (index <= 0)
                    return;
            }
        }
    }

}