using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada
{
    public class ScriptableQueue<T> : SerializedScriptableObject, IQueue<T>
    {
        [SerializeField] private IQueue<T> queue;

        public int Count => this.queue.Count;

        public void Enqueue(T item) => this.queue.Enqueue(item);

        public T Dequeue() => this.queue.Dequeue();

        public T Peek() => this.queue.Peek();
        public void Clear() => this.queue.Clear();

        public IEnumerator<T> GetEnumerator() => this.queue.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) this.queue).GetEnumerator();
    }
}