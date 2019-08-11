using System.Collections.Generic;

namespace Chinchillada.Utilities
{
    using System;
    
    public class ObservableCollection<T> : CollectionWrapper<T>
    {
        public event Action<T> ItemAdded;
        public event Action<T> ItemRemoved;
        public event Action Cleared;

        public ObservableCollection(ICollection<T> collection) : base(collection)
        {
        }

        public override void Add(T item)
        {
            base.Add(item);
            this.ItemAdded?.Invoke(item);
        }

        public override bool Remove(T item)
        {
            if (!base.Remove(item))
                return false;
            
            this.ItemRemoved?.Invoke(item);
            return true;
        }

        public override void Clear()
        {
            base.Clear();
            this.Cleared?.Invoke();
        }
    }
}