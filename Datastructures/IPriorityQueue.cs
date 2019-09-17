using System;
using System.Collections.Generic;

namespace Chinchillada.Utilities
{
    public interface IPriorityQueue<TItem, in TPriority> : IEnumerable<TItem>
        where TPriority : IComparable<TPriority>
    {
        int Count { get; }
        
        TItem Peek { get; }
        
        void Enqueue(TItem item, TPriority priority);

        TItem Dequeue();

        void Clear();
    }
}