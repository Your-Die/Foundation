using System.Collections;
using System.Collections.Generic;

namespace Chinchillada
{
    public class FIFOCollection<T> : IQueueCollection<T>
    {
        private readonly LinkedList<T> list = new LinkedList<T>();

        public int  Count      => this.list.Count;
        
        public bool IsReadOnly => false;

        public void Enqueue(T item) => this.list.AddLast(item);

        public T Dequeue()
        {
            var value = this.Peek();
            this.list.RemoveFirst();

            return value;
        }

        public T Peek() => this.list.Count > 0 ? this.list.First.Value : default;

        public bool Remove(T item) => this.list.Remove(item);

        public void Clear() => this.list.Clear();
        
        public bool Contains(T item) => this.list.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => this.list.CopyTo(array, arrayIndex);

        void ICollection<T>.Add(T item) => this.Enqueue(item);
        
        public IEnumerator<T> GetEnumerator() => new FrontToBackEnumerator<T>(this.list);

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}