using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// Class containing extension methods used for generation.
    /// </summary>
    public static class FuncExtensions
    {
        /// <summary>
        /// Returns an infinite <see cref="IEnumerable{T}"/> using the <paramref name="factoryMethod"/>.
        /// </summary>
        public static IEnumerable<T> Generate<T>(this Func<T> factoryMethod)
        {
            while (true)
            {
                yield return factoryMethod();
            }
        }

        /// <summary>
        /// Generates <paramref name="amount"/> of items using the <paramref name="factoryMethod"/>.
        /// </summary>
        public static IEnumerable<T> Generate<T>(this Func<T> factoryMethod, int amount)
        {
            return factoryMethod.Generate().Take(amount);
        }

        /// <summary>
        /// Attempts to generate <paramref name="amount"/> of items with the <paramref name="factoryMethod"/> that satisfy the <paramref name="validator"/>.
        /// </summary>
        public static IEnumerable<T> GenerateValid<T>(this Func<T> factoryMethod, Func<T, bool> validator, int amount)
        {
            return factoryMethod.Generate()
                                .Where(validator)
                                .Take(amount);
        }

        public static float Area(
            this Func<float, float> function, float start = 0f, float end = 1, int buckets = 1000) 
            => Enumerable.Range(0, buckets)
                .Select(i =>
                {
                    var value = start + (end - start) * i / buckets;
                    return function(value) / buckets;
                }).Sum();
    }
}
