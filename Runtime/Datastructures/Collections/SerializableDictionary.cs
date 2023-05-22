using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>, 
                                                        ISerializationCallbackReceiver
    {
        [SerializeField, TableList] private Entry[] entries;

        private Dictionary<TKey, TValue> dictionary;

        public SerializableDictionary(IEnumerable<(TKey, TValue)> pairs)
        {
            this.entries = pairs.Select(pair => new Entry(pair.Item1, pair.Item2)).ToArray();
        }


        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            this.dictionary = this.entries.ToDictionary(
                entry => entry.key, 
                entry => entry.value);
        }
        
        #region IDictionary (Delegates to dictionary)

        public TValue this[TKey key] => this.dictionary[key];

        public IEnumerable<TKey> Keys => this.dictionary.Keys;

        public IEnumerable<TValue> Values => this.dictionary.Values;
        
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.dictionary).GetEnumerator();
        }

        public int Count => this.dictionary.Count;

        public bool ContainsKey(TKey key)
        {
            return this.dictionary.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.dictionary.TryGetValue(key, out value);
        }

        #endregion

        
        [Serializable]
        private class Entry
        {
            public TKey key;

            public TValue value;

            public Entry(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
            }
        }
    }
}