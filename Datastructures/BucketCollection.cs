using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada.Foundation
{
    public class BucketCollection<TKey, TValue> : IDictionary<TKey, ICollection<TValue>>
    {
        private readonly IDictionary<TKey, ICollection<TValue>> buckets;
        private IDictionary<TKey, ICollection<TValue>> dictionaryImplementation;

        public int Count => this.buckets.Count;

        public bool IsReadOnly => this.buckets.IsReadOnly;
        
        public bool TryGetValue(TKey key, out ICollection<TValue> value)
        {
            return this.buckets.TryGetValue(key, out value);
        }

        public ICollection<TValue> this[TKey key]
        {
            get => this.buckets[key];
            set => this.buckets[key] = value;
        }

        public ICollection<TKey> Keys => this.buckets.Keys;

        public ICollection<ICollection<TValue>> Values => this.buckets.Values;

        public BucketCollection() : this(() => new List<TValue>())
        {
        }

        public BucketCollection(Func<ICollection<TValue>> collectionFactory)
        {
            this.buckets = new DefaultDictionary<TKey, ICollection<TValue>>(collectionFactory);
        }

        public BucketCollection(IDictionary<TKey, ICollection<TValue>> buckets)
        {
            this.buckets = buckets;
        }

        public void Add(TKey key, TValue value) => this.buckets[key].Add(value);

        public bool Remove(TKey key) => this.buckets.Remove(key);

        public bool Remove(TKey key, TValue value)
        {
            var bucket = this.buckets[key];
            return bucket != null && bucket.Remove(value);
        }

        public void Add(TKey key, ICollection<TValue> value) => this.buckets.Add(key, value);

        public void Clear() => this.buckets.Clear();

        public bool ContainsKey(TKey key) => this.buckets.ContainsKey(key);

        public bool Contains(TKey key, TValue value)
        {
            return this.buckets.TryGetValue(key, out var bucket) && bucket.Contains(value);
        }
        public bool Contains(TKey key, Func<TValue, bool> predicate)
        {
            return this.buckets.TryGetValue(key, out var bucket) && bucket.Any(predicate);
        }
        
        public IEnumerator<KeyValuePair<TKey, ICollection<TValue>>> GetEnumerator() => this.buckets.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) this.buckets).GetEnumerator();

        public void Add(KeyValuePair<TKey, ICollection<TValue>> item) => this.buckets.Add(item);

        public bool Contains(KeyValuePair<TKey, ICollection<TValue>> item) => this.buckets.Contains(item);

        public void CopyTo(KeyValuePair<TKey, ICollection<TValue>>[] array, int arrayIndex)
        {
            this.buckets.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, ICollection<TValue>> item) => this.buckets.Remove(item);


    }
}