using System.Collections;
using System.Collections.Generic;

namespace Chinchillada
{
    /// <summary>
    /// Queue that dequeues in a randomized order.
    /// </summary>
    public class RandomQueue<T> : IQueue<T>
    {
        private readonly IRNG random;
        private readonly List<T> list = new();

        /// <summary>
        /// We store a peeked index to facilitate Peek() to an otherwise random selection during Dequeue().
        /// This index is invalidated if the queue is changed in any way, such as with an Enqueue, Dequeue or clear.
        /// </summary>
        private int? peekedIndex;
        
        public int Count => this.list.Count;

        public RandomQueue(IRNG random) => this.random = random;

        public void Enqueue(T item)
        {
            this.list.Add(item);
            this.InvalidatePeek();
        }

        public T Dequeue()
        {
            if (!this.peekedIndex.HasValue)
                return this.list.GrabRandom(this.random);

            var result = this.list.Extract(this.peekedIndex.Value);
            this.InvalidatePeek();
            
            return result;
        }

        public T Peek()
        {
            this.peekedIndex ??= this.list.ChooseRandomIndex(this.random);
            return this.list[this.peekedIndex.Value];
        }

        public void Clear()
        {
            this.list.Clear();
            this.InvalidatePeek();
        }

        private void InvalidatePeek() => this.peekedIndex = null;
        
        public IEnumerator<T> GetEnumerator() => this.list.Shuffled(this.random).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}