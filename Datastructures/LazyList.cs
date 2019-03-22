using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Utilities
{
    public class LazyList<T> : IList<T>
    {
        private readonly Queue<IEnumerable<T>> _enumerables = new Queue<IEnumerable<T>>();

        private readonly List<T> _list = new List<T>();

        private IEnumerator<T> _enumerator;

        public bool IsReadOnly => false;

        public int Count
        {
            get
            {
                int count = 0;

                foreach (T element in this)
                    count++;

                return count;
            }
        }

        public LazyList(IEnumerable<T> enumerable) => _enumerables.Enqueue(enumerable);

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var p in EnumerateList())
                yield return p;

            foreach (var p1 in EnumerateCurrent())
                yield return p1;

            foreach (var p2 in EnumerateQueue())
                yield return p2;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(T item)
        {
            if (_enumerator == null && _enumerables.IsEmpty())
            {
                _list.Add(item);
            }
            else
            {
                IEnumerable<T> enumerable = Enumerables.Single(item);
                _enumerables.Enqueue(enumerable);
            }
        }

        public void AddRange(IEnumerable<T> range) => _enumerables.Enqueue(range);

        public void Clear()
        {
            _list.Clear();
            _enumerator = null;
            _enumerables.Clear();
        }

        public bool Contains(T item) => this.IndexOf(item) > 0;

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (T element in this)
            {
                array[arrayIndex++] = element;
            }
        }

        public bool Remove(T item)
        {
            int index = this.IndexOf(item);

            if (index < 0)
                return false;

            this.RemoveAt(index);
            return true;
        }

        public int IndexOf(T item)
        {
            Comparer<T> comparer = Comparer<T>.Default;
            bool IsSame(T element) => comparer.Compare(item, element) == 0;

            int index = 0;
            foreach (T element in this)
            {
                if (IsSame(element))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            EnumerateUntil(index);
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            EnumerateUntil(index);
            _list.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                EnumerateUntil(index);
                return _list[index];
            }
            set
            {
                EnumerateUntil(index);
                _list[index] = value;
            }
        }

        private IEnumerable<T> EnumerateQueue()
        {
            // Go to the queued enumerables.
            while (_enumerables.Any())
            {
                var enumerable = _enumerables.Dequeue();
                _enumerator = enumerable.GetEnumerator();

                do
                {
                    yield return _enumerator.Current;
                } while (_enumerator.MoveNext());
            }

            _enumerator = null;
        }

        private IEnumerable<T> EnumerateCurrent()
        {
            // go through the current enumerator.
            while (_enumerator.MoveNext())
            {
                T element = _enumerator.Current;
                _list.Add(element);

                yield return element;
            }

            _enumerator = null;
        }

        private IEnumerable<T> EnumerateList()
        {
            // First loop through the elements we already enumerated.
            foreach (T element in _list)
                yield return element;
        }

        private void EnumerateUntil(int index)
        {
            index -= _list.Count;
            if (index <= 0)
                return;

            foreach (var unused in EnumerateCurrent())
            {
                index--;
                if (index <= 0)
                    return;
            }

            foreach (var unused in EnumerateQueue())
            {
                index--;
                if (index <= 0)
                    return;
            }
        }
    }
}