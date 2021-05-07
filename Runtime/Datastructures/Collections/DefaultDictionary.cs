using System;
using System.Collections;
using System.Collections.Generic;

namespace Chinchillada
{
    /// <summary>
    /// <see cref="IDictionary{TKey,TValue}"/> that initializes default values when a new key is introduced.
    /// </summary>
    public class DefaultDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly Func<TKey, TValue> defaultConstructor;
        private readonly Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
        
        public bool IsReadOnly => false;
        public int Count => this.dictionary.Count;

        public ICollection<TKey> Keys => this.dictionary.Keys;
        public ICollection<TValue> Values => this.dictionary.Values;
        public TValue this[TKey key]
        {
            get
            {
                if (!this.dictionary.ContainsKey(key)) 
                    this.dictionary[key] = this.defaultConstructor.Invoke(key);

                return this.dictionary[key];
            }
            set => this.dictionary[key] = value;
        }

        public DefaultDictionary(Func<TKey, TValue> defaultConstructor)
        {
            this.defaultConstructor = defaultConstructor;
        }
        
        public DefaultDictionary(Func<TValue> defaultConstructor)
        {
            this.defaultConstructor = _ => defaultConstructor.Invoke();
        }

        public DefaultDictionary(TValue defaultValue)
        {
            this.defaultConstructor = _ => defaultValue;
        }
        
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            var collection = (ICollection<KeyValuePair<TKey, TValue>>) this.dictionary;
            collection.CopyTo(array, arrayIndex);
        }

        public void Add(TKey key, TValue value) => this.dictionary.Add(key, value);

        public bool ContainsKey(TKey key) => this.dictionary.ContainsKey(key);

        public bool Remove(TKey key) => this.dictionary.Remove(key);

        public bool TryGetValue(TKey key, out TValue value) => this.dictionary.TryGetValue(key, out value);

        public void Add(KeyValuePair<TKey, TValue> item) => this.dictionary.Add(item.Key, item.Value);

        public void Clear() => this.dictionary.Clear();

        public bool Contains(KeyValuePair<TKey, TValue> item) => this.dictionary.ContainsKey(item.Key);

        public bool Remove(KeyValuePair<TKey, TValue> item) => this.dictionary.Remove(item.Key);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => this.dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public override string ToString() => this.dictionary.ToString();
    }
}