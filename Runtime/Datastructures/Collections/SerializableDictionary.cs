using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, 
                                                        IReadOnlyDictionary<TKey, TValue>, 
                                                        ISerializationCallbackReceiver
    {
        [SerializeField] private Entry[] entries;

        private IDictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        public TValue this[TKey key]
        {
            get => this.dictionary[key];
            set => this.dictionary[key] = value;
        }

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => this.Keys;

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => this.Values;

        public ICollection<TKey> Keys => this.dictionary.Keys;

        public ICollection<TValue> Values => this.dictionary.Values;
        
        public bool IsReadOnly => this.dictionary.IsReadOnly;
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            this.dictionary = this.entries.ToDictionary(entry => entry.key, entry => entry.value);
        }

  
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.dictionary).GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.dictionary.Add(item);
        }

        public void Clear()
        {
            this.dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            this.dictionary.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.dictionary.Remove(item);
        }

        public int Count => this.dictionary.Count;


        public void Add(TKey key, TValue value)
        {
            this.dictionary.Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return this.dictionary.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            return this.dictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.dictionary.TryGetValue(key, out value);
        }

        [Serializable]
        private class Entry
        {
            public TKey key;

            public TValue value;
        }

    }
}