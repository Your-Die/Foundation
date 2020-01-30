using System;
using System.Collections.Generic;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Sorts items of <typeparamref name="TValue"/> into buckets identified by a given <see cref="bucketSelector"/>
    /// function.
    /// </summary>
    public class BucketSet<TKey, TValue>
    {
        /// <summary>
        /// Sorts items into buckets.
        /// </summary>
        private readonly Func<TValue, TKey> bucketSelector;
        
        /// <summary>
        /// The buckets.
        /// </summary>
        private readonly IDictionary<TKey, List<TValue>> buckets;

        /// <summary>
        /// Get the list of <see cref="TValue"/> sorted into the bucket identified by the <paramref name="bucket"/>.
        /// </summary>
        public IEnumerable<TValue> this[TKey bucket] => this.buckets[bucket];

        /// <summary>
        /// The bucket identifiers.
        /// </summary>
        public IEnumerable<TKey> Buckets => this.buckets.Keys;

        public BucketSet(Func<TValue, TKey> bucketSelector)
        {
            this.buckets = new DefaultDictionary<TKey, List<TValue>>(BucketConstructor);
            this.bucketSelector = bucketSelector;
            
            List<TValue> BucketConstructor() => new List<TValue>();
        }

        public BucketSet(IEnumerable<TValue> enumerable, Func<TValue, TKey> bucketSelector) 
            : this(bucketSelector)
        {
            foreach (var item in enumerable) 
                this.Add(item);
        }

        /// <summary>
        /// Add the <paramref name="value"/> to this <see cref="BucketSet{TKey,TValue}"/>.
        /// </summary>
        /// <returns>The key of the bucket it was sorted into.</returns>
        public TKey Add(TValue value)
        {
            var bucket = this.bucketSelector.Invoke(value);
            this.buckets[bucket].Add(value);

            return bucket;
        }
    }
}