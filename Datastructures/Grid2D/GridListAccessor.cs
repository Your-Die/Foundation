using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Grid
{
    public abstract class GridListAccessor : IList<int>
    {
        public Grid2D Grid { get; set; }
        
        public bool IsReadOnly => false;

        
        public abstract int Count { get; }

        public abstract int this[int index] { get; set; }

        protected GridListAccessor(Grid2D grid) => this.Grid = grid;
        
        
        public bool Contains(int item) => this.Any(element => element == item);

        public void CopyTo(int[] array, int arrayIndex)
        {
            for (var index = 0; index < this.Count; index++)
                array[arrayIndex + index] = this[index];
        }

        public int IndexOf(int item)
        {
            for (var index = 0; index < this.Count; index++)
            {
                if (this[index] == item)
                    return index;
            }

            return -1;
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (var x = 0; x < this.Count; x++)
                yield return this[x];
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(int item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Remove(int item)
        {
            throw new NotSupportedException();
        }

        public void Insert(int index, int item)
        {
            throw new NotSupportedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }
    }
}