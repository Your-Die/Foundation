using System;
using System.Collections.Generic;

namespace Chinchillada.Foundation
{
    public class BucketSet<TKey, TValue>
    {
        private readonly Func<TValue, TKey> bucketSelector;
        private readonly IDictionary<TKey, List<TValue>> buckets;

        public IEnumerable<TValue> this[TKey bucket] => this.buckets[bucket];

        public IEnumerable<TKey> Buckets => this.buckets.Keys;

        public BucketSet(IEnumerable<TValue> collection, Func<TValue, TKey> bucketSelector)
            : this(bucketSelector)
        {
            foreach (var item in collection)
                this.Add(item);
        }

        public BucketSet(Func<TValue, TKey> bucketSelector)
        {
            this.buckets = new DefaultDictionary<TKey, List<TValue>>(BucketConstructor);
            this.bucketSelector = bucketSelector;

            List<TValue> BucketConstructor() => new List<TValue>();
        }

        public TKey Add(TValue value)
        {
            var bucket = this.bucketSelector.Invoke(value);
            this.buckets[bucket].Add(value);

            return bucket;
        }
    }
}