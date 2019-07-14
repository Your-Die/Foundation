using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Utilities
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Creates a list collection of <see cref="ValueTuple"/> from the <paramref name="dictionary"/>.
        /// </summary>
        public static IEnumerable<(TElement, TValue)> ToTuples<TElement, TValue>(
            this IDictionary<TElement, TValue> dictionary)
        {
            return dictionary.Keys.Select(key => (key, dictionary[key]));
        }

        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary, 
            TKey key,
            TValue defaultValue = default)
        {
            return dictionary.TryGetValue(key, out TValue value) ? value : defaultValue;
        }

        public static T Best<T>(this IDictionary<T, float> dictionary)
        {
            return dictionary.Keys.Best(key => dictionary[key]);
        }
    }
}