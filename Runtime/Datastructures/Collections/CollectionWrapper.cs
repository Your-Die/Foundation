namespace Chinchillada
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Base class for wrappers of <see cref="ICollection{T}"/>. Makes it easy to only override what a wrapper needs.
    /// </summary>
    public abstract class CollectionWrapper<T> : ICollection<T>
    {
        public virtual int Count => this.Collection.Count;
        public virtual bool IsReadOnly => this.Collection.IsReadOnly;
        protected ICollection<T> Collection { get; }

        protected CollectionWrapper(ICollection<T> collection = null)
        {
            this.Collection = collection ?? new List<T>();
        }
        
        public virtual IEnumerator<T> GetEnumerator() => this.Collection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public virtual void Add(T item) => this.Collection.Add(item);

        public virtual void Clear() => this.Collection.Clear();

        public virtual bool Contains(T item) => this.Collection.Contains(item);

        public virtual void CopyTo(T[] array, int arrayIndex) => this.Collection.CopyTo(array, arrayIndex);

        public virtual bool Remove(T item) => this.Collection.Remove(item);
    }
}