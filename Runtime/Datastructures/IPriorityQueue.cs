using System;
using System.Collections.Generic;

namespace _Projects.State_Machine.Utility
{
    public interface IPriorityQueue<TItem, in TPriority> : IReadOnlyCollection<TItem>
        where TPriority : IComparable<TPriority>
    {
        void Add(TItem content, TPriority priority);

        TItem Pop();

        TItem Peek();
    }
}