using System.Collections.Generic;
using System.Linq;

namespace Foundation.Algorithms
{
    public static class Permutations
    {
        public static IEnumerable<T[]> Generate<T>(IList<T> items, int permutationSize)
        {
            var length = 1;
            var generation = items.Select(item => new[] {item});

            while (length < permutationSize)
            {
                length++;
                generation = GenerateGeneration(generation, items, length);
            }

            return generation;
        }

        public static IEnumerable<T[]> GenerateUntil<T>(IList<T> items, int maxLength)
        {
            var length = 1;
            var generation = items.Select(item => new[] {item}).ToList();
            foreach (var permutation in generation)
                yield return permutation;

            while (length < maxLength)
            {
                length++;
                var nextGeneration = new List<T[]>();

                foreach (var item in GenerateGeneration(generation, items, length))
                {
                    yield return item;
                    nextGeneration.Add(item);
                }

                generation = nextGeneration;
            }
        }

        private static IEnumerable<T[]> GenerateGeneration<T>(IEnumerable<T[]> prevGeneration, IList<T> items, int length)
        {
            foreach (var permutation in prevGeneration)
            {
                var permutationTemplate = new T[length];
                permutation.CopyTo(permutationTemplate, 0);

                foreach (var item in items)
                {
                    var newPermutation = (T[]) permutationTemplate.Clone();
                    newPermutation[length - 1] = item;
                        
                    yield return newPermutation;
                }
            }
        }
    }
}