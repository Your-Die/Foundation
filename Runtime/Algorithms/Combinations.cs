using System;
using System.Collections.Generic;
using Chinchillada;

namespace Chinchillada
{
    public static class Combinations
    {
        /// <summary>
        /// Generates all combinations of the <paramref name="items"/>
        /// of lengths up to and including the length of the <paramref name="items"/> array.
        /// </summary>
        /// <param name="items">The items to generate combinations of.</param>
        /// <typeparam name="T">The type of items.</typeparam>
        /// <returns>All the combinations of lengths in the interval of [0, <paramref name="items"/>.Length].</returns>
        public static IEnumerable<T[]> Generate<T>(IList<T> items)
        {
            return Generate(items, items.Count);
        }

        /// <summary>
        /// Generates all combinations of the <paramref name="items"/>
        /// of lengths up to and including <paramref name="maxLength"/>.
        /// </summary>
        /// <param name="items">The items to generate combinations of.</param>
        /// <param name="maxLength">The highest length of combinations we want to generate.</param>
        /// <typeparam name="T">The type of items.</typeparam>
        /// <returns>All the combinations of lengths in the interval of [0, <paramref name="maxLength"/>].</returns>
        public static IEnumerable<T[]> Generate<T>(IList<T> items, int maxLength)
        {
            // The combination of length 0 (Empty).
            yield return new T[0];
            
            // Generate the length-1 combinations (The individual elements).
            var layer = new List<int[]>();
            for (var index = 0; index < items.Count; index++)
            {
                var indexSet = new[] {index};
                yield return InterpretIndices(indexSet);
                layer.Add(indexSet);
            }

            // Generate the combinations of lengths [2, maxLength].
            for (var combinationLength = 1; combinationLength < maxLength; combinationLength++)
            {
                var nextLayer = new List<int[]>();

                foreach (var combination in layer)
                {
                    var lastIndex = combination.LastIndex();
                    var lastValue = combination[lastIndex];
                    var newIndex = lastIndex + 1;

                    for (var newValue = lastValue + 1; newValue < maxLength; newValue++)
                    {
                        var newCombination = new int[combination.Length + 1];
                        Array.Copy(combination, 0, newCombination, 0, combination.Length);

                        newCombination[newIndex] = newValue;

                        yield return InterpretIndices(newCombination);
                        nextLayer.Add(newCombination);
                    }
                }
                
                layer = nextLayer;
            }

            T[] InterpretIndices(IReadOnlyList<int> indices)
            {
                var combination = new T[indices.Count];
                for (var i = 0; i < indices.Count; i++)
                {
                    var index = indices[i];
                    combination[i] = items[index];
                }

                return combination;
            }
        }
    }
}