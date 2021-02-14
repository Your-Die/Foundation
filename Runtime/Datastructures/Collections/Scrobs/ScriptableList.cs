using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Foundation
{
    [CreateAssetMenu(menuName = "Scrobs/Collections/List")]
    public class ScriptableList<T> : SerializedScriptableObject, IList<T>
    {
        [SerializeField] private IList<T> list = new List<T>();
        
        public int Count => this.list.Count;

        public bool IsReadOnly => this.list.IsReadOnly;
        
        public T this[int index]
        {
            get => this.list[index];
            set => this.list[index] = value;
        }

        public void Add(T item) => this.list.Add(item);

        public void Clear() => this.list.Clear();

        public bool Contains(T item) => this.list.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => this.list.CopyTo(array, arrayIndex);

        public bool Remove(T item) => this.list.Remove(item);

        public int IndexOf(T item) => this.list.IndexOf(item);

        public void Insert(int index, T item) => this.list.Insert(index, item);

        public void RemoveAt(int index) => this.list.RemoveAt(index);

              
        public IEnumerator<T> GetEnumerator() => this.list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) this.list).GetEnumerator();
    }
}