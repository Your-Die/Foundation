using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada
{
    public class PriorityQueue<TContent, TPriority> : IPriorityQueue<TContent, TPriority> 
        where TPriority : IComparable<TPriority>
    {
        private readonly SortedSet<Item> set = new SortedSet<Item>(new ItemComparer());

        public int Count => this.set.Count;
        
        public IEnumerator<TContent> GetEnumerator()
        {
            return this.set
                       .Reverse()
                       .Select(item => item.Content)
                       .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(TContent content, TPriority priority)
        {
            var item = new Item {Content = content, Priority = priority};
            this.set.Add(item);
        }

        public TContent Pop()
        {
            var highestPriority = this.set.Max;
            this.set.Remove(highestPriority);

            return highestPriority.Content;
        }

        public TContent Peek()
        {
            var highestPriority = this.set.Max;
            return highestPriority.Content;
        }

        public void Clear() => this.set.Clear();

        private struct Item
        {
            public TContent Content { get; set; }
            
            public TPriority Priority { get; set; }
        }
        
        private class ItemComparer : IComparer<Item>
        {
            public int Compare(Item x, Item y) => x.Priority.CompareTo(y.Priority);
        }
    }
}