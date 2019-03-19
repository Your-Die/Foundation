using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Utilities
{
    public static class EnumerableExtensions
    {
        public static T[] EnsureArray<T>(this IEnumerable<T> enumerable)
        {
            return enumerable as T[] ?? enumerable.ToArray();
        }

        public static IList<T> EnsureList<T>(this IEnumerable<T> enumerable)
        {
            return enumerable as IList<T> ?? enumerable.ToList();
        }

        /// <summary>
        /// Checks if the <paramref name="enumerable"/> is empty.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <paramref name="enumerable"/>.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>Whether the <paramref name="enumerable"/> is empty or not.</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.Any();
        }

        /// <summary>
        /// Finds the element in the <paramref name="enumerable"/> that scores the best with the <paramref name="scoreFunction"/>.
        /// </summary> 
        public static T Best<T>(this IEnumerable<T> enumerable, Func<T, float> scoreFunction)
        {
            var array = enumerable.EnsureArray();
            int index = array.IndexOfBest(scoreFunction);

            return index >= 0 ? array[index] : default;
        }

        public static T Worst<T>(this IEnumerable<T> enumerable, Func<T, float> scoreFunction)
        {
            var array = enumerable.EnsureArray();
            int index = array.IndexOfWorst(scoreFunction);

            return index >= 0 ? array[index] : default;
        }

        public static int IndexOfBest<T>(this IEnumerable<T> enumerable, Func<T, float> scoreFunction)
        {
            //Cast to list so we only enumerate once and can use indexing.
            var array = enumerable.EnsureArray();

            //Ensure the list is filled.
            if (array.IsEmpty())
                return -1;

            //Start with first element.
            int bestIndex = 0;
            float bestScore = scoreFunction(array[bestIndex]);

            //Find best scoring element.
            for (int index = 1; index < array.Length; index++)
            {
                //Get element and score.
                T element = array[index];
                float score = scoreFunction(element);

                //Compare and set.
                if (score <= bestScore)
                    continue;
                bestIndex = index;
                bestScore = score;
            }

            return bestIndex;
        }


        public static int IndexOfWorst<T>(this IEnumerable<T> enumerable, Func<T, float> scoreFunction)
        {
            //Cast to list so we only enumerate once and can use indexing.
            var array = enumerable.EnsureArray();

            //Ensure the list is filled.
            if (array.IsEmpty())
                return -1;

            //Start with first element.
            int worstIndex = 0;
            float worstScore = scoreFunction(array[worstIndex]);

            //Find best scoring element.
            for (int index = 1; index < array.Length; index++)
            {
                //Get element and score.
                T element = array[index];
                float score = scoreFunction(element);

                //Compare and set.
                if (score >= worstScore)
                    continue;
                worstIndex = index;
                worstScore = score;
            }

            return worstIndex;
        }

        public static LinkedList<T> ToLinked<T>(this IEnumerable<T> enumerable)
        {
            var linkedList = new LinkedList<T>();
            foreach (T element in enumerable)
            {
                linkedList.AddLast(element);
            }

            return linkedList;
        }

        public static IEnumerable<(TElement, TValue)> ToTuples<TElement, TValue>(this IDictionary<TElement, TValue> dictionary)
        {
            foreach (var key in dictionary.Keys)
            {
                var element = dictionary[key];
                yield return (key, element);
            }
        }

        public static (int, int) MinMax<T>(this IEnumerable<T> enumerable, Func<T, int> selector)
        {
            int min = int.MaxValue;
            int max = int.MinValue;

            foreach (T element in enumerable)
            {
                int value = selector(element);

                if (value < min)
                    min = value;

                if (value > max)
                    max = value;
            }

            return (min, max);
        }
    }
}