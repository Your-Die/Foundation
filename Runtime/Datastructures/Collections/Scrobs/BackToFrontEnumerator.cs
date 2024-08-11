using System.Collections;
using System.Collections.Generic;

namespace Chinchillada
{
    public class BackToFrontEnumerator<T> : IEnumerator<T>
    {
        private readonly LinkedList<T> list;

        private LinkedListNode<T> node;

        
        public T Current => this.node.Value;

        object IEnumerator.Current => this.Current;
        
        public BackToFrontEnumerator(LinkedList<T> list)
        {
            this.list = list;
        }

        public bool MoveNext()
        {
            this.node = this.node != null 
                ? this.node.Previous 
                : this.list.Last;
            
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