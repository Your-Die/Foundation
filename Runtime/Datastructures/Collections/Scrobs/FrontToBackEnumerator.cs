using System.Collections;
using System.Collections.Generic;

namespace Chinchillada
{
    public class FrontToBackEnumerator<T> : IEnumerator<T>
    {
        private readonly LinkedList<T> list;

        private LinkedListNode<T> node = null;

        public T Current => this.node.Value;

        object IEnumerator.Current => this.Current;

        public FrontToBackEnumerator(LinkedList<T> list)
        {
            this.list = list;
        }
            
        public bool MoveNext()
        {
            this.node = this.node == null 
                ? this.list.First 
                : this.node.Next;
                    
            return this.node != null;
        }

        public void Reset()
        {
            this.node = null;
        }

        public void Dispose()
        {
        }
    }
}