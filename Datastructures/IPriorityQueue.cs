using System;
using System.Collections.Generic;

namespace Chinchillada.Utilities
{
    public interface IPriorityQueue<TItem, in TPriority> : IEnumerable<TItem>
        where TPriority : IComparable<TPriority>
    {
        int Count { get; }
        
        TItem First { get; }
        
        void Enqueue(TItem item, TPriority priority);

        TItem Dequeue();

        void Clear();

        bool Remove(TItem item);

        bool Contains(TItem item);

        void UpdatePriority(TItem item, TPriority priority);
    }
}