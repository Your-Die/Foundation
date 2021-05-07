using System;
using System.Collections.Generic;

namespace Chinchillada
{
    /// <summary>
    /// Taken from https://gist.github.com/paralleltree/31045ab26f69b956052c
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class CustomPriorityQueue<T> where T : IComparable<T>
    {
        private List<T> list;
        public int Count => this.list.Count;

        public bool HasNext => this.Count > 0;

        public readonly bool IsDescending;

        public CustomPriorityQueue() => this.list = new List<T>();

        public CustomPriorityQueue(bool isDescending)
            : this()
        {
            this.IsDescending = isDescending;
        }

        public CustomPriorityQueue(int capacity, bool isDescending = false)
        {
            this.list = new List<T>(capacity);
            this.IsDescending = isDescending;
        }

        public CustomPriorityQueue(IEnumerable<T> collection, bool isDescending = false)
            : this()
        {
            this.IsDescending = isDescending;

            foreach (var item in collection)
                this.Enqueue(item);
        }


        public void Enqueue(T x)
        {
            this.list.Add(x);
            int i = this.Count - 1;

            while (i > 0)
            {
                int p = (i - 1) / 2;
                if ((this.IsDescending ? -1 : 1) * this.list[p].CompareTo(x) <= 0) break;

                this.list[i] = this.list[p];
                i = p;
            }

            if (this.Count > 0) this.list[i] = x;
        }

        public T Dequeue()
        {
            var target = this.Peek();
            var root = this.list[this.Count - 1];
            this.list.RemoveAt(this.Count - 1);

            var i = 0;
            while (i * 2 + 1 < this.Count)
            {
                var a = i * 2 + 1;
                var b = i * 2 + 2;
                var c = b < this.Count && (this.IsDescending ? -1 : 1) * this.list[b].CompareTo(this.list[a]) < 0
                    ? b
                    : a;

                if ((this.IsDescending ? -1 : 1) * this.list[c].CompareTo(root) >= 0) break;
                this.list[i] = this.list[c];
                i = c;
            }

            if (this.Count > 0) this.list[i] = root;
            return target;
        }

        public T Peek()
        {
            if (this.Count == 0) throw new InvalidOperationException("Queue is empty.");
            return this.list[0];
        }

        public void Clear() => this.list.Clear();

        public bool Contains(T item) => this.list.Contains(item);

        public bool Any() => this.HasNext;
    }
}