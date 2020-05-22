using System;
using System.Collections.Generic;
using Chinchillada.Foundation;

namespace Foundation.Algorithms
{
    public static class Combinations
    {
        public static IEnumerable<T[]> Generate<T>(T[] items)
        {
            return Generate(items, items.Length);
        }

        public static IEnumerable<T[]> Generate<T>(T[] items, int k)
        {
            var layer = new List<int[]>();
            for (int index = 0; index < items.Length; index++)
            {
                var indexSet = new[] {index};
                yield return InterpretIndices(indexSet);
                layer.Add(indexSet);
            }

            for (int layerIndex = 1; layerIndex < k; layerIndex++)
            {
                var nextLayer = new List<int[]>();

                foreach (var combination in layer)
                {
                    var lastIndex = combination.LastIndex();
                    var lastValue = combination[lastIndex];
                    var newIndex = lastIndex + 1;

                    for (var newValue = lastValue + 1; newValue < k; newValue++)
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

            T[] InterpretIndices(int[] indices)
            {
                var combination = new T[indices.Length];
                for (var i = 0; i < indices.Length; i++)
                {
                    var index = indices[i];
                    combination[i] = items[index];
                }

                return combination;
            }
        }
    }
}