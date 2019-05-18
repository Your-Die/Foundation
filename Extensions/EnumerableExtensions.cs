﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Class containing extension methods for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Ensures the <paramref name="enumerable"/> is an array.
        /// First attempts to cast it and if that fails calls <see cref="IEnumerable{T}.ToArray()"/>
        /// </summary>
        /// <typeparam name="T">The type of elements in the <paramref name="enumerable"/>.</typeparam>
        /// <param name="enumerable">The <see cref="IEnumerable{T}"/> that we want to ensure as an array.</param>
        /// <returns>The <paramref name="enumerable"/> as array.</returns>
        public static T[] EnsureArray<T>(this IEnumerable<T> enumerable)
        {
            return enumerable as T[] ?? enumerable.ToArray();
        }

        /// <summary>
        /// Ensures the <paramref name="enumerable"/> is a <see cref="List{T}"/>.
        /// First attempts to cast it and if that fails calls <see cref="IEnumerable{T}.ToList()"/>
        /// </summary>
        /// <typeparam name="T">The type of elements in the <paramref name="enumerable"/>.</typeparam>
        /// <param name="enumerable">The <see cref="IEnumerable{T}"/> that we want to ensure as a <see cref="List{T}"/>.</param>
        /// <returns>The <paramref name="enumerable"/> as <see cref="List{T}"/>.</returns>
        public static IList<T> EnsureList<T>(this IEnumerable<T> enumerable)
        {
            return enumerable as IList<T> ?? enumerable.ToList();
        }


        /// <summary>
        /// Ensures the <paramref name="enumerable"/> is a <see cref="LinkedList{T}"/>.
        /// First attempts to cast it and if that fails calls ToLinked.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <paramref name="enumerable"/>.</typeparam>
        /// <param name="enumerable">The <see cref="IEnumerable{T}"/> that we want to ensure as a <see cref="List{T}"/>.</param>
        /// <returns>The <paramref name="enumerable"/> as <see cref="List{T}"/>.</returns>
        public static LinkedList<T> EnsureLinked<T>(this IEnumerable<T> enumerable)
        {
            return enumerable as LinkedList<T> ?? enumerable.ToLinked();
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

        /// <summary>
        /// Finds the element in the <paramref name="enumerable"/> that scores the worst with the <paramref name="scoreFunction"/>.
        /// </summary> 
        public static T Worst<T>(this IEnumerable<T> enumerable, Func<T, float> scoreFunction)
        {
            var array = enumerable.EnsureArray();
            int index = array.IndexOfWorst(scoreFunction);

            return index >= 0 ? array[index] : default;
        }

        /// <summary>
        /// Finds the index of the element in the <paramref name="enumerable"/> that scores the best with the <paramref name="scoreFunction"/>.
        /// </summary> 
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

        /// <summary>
        /// Finds the index of the element in the <paramref name="enumerable"/> that scores the worst with the <paramref name="scoreFunction"/>.
        /// </summary> 
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

        /// <summary>
        /// Creates a <see cref="LinkedList{T}"/> from the <paramref name="enumerable"/>.
        /// </summary>
        public static LinkedList<T> ToLinked<T>(this IEnumerable<T> enumerable)
        {
            var linkedList = new LinkedList<T>();
            foreach (T element in enumerable)
            {
                linkedList.AddLast(element);
            }

            return linkedList;
        }

        /// <summary>
        /// Creates a list collection of <see cref="ValueTuple"/> from the <paramref name="dictionary"/>.
        /// </summary>
        public static IEnumerable<(TElement, TValue)> ToTuples<TElement, TValue>(this IDictionary<TElement, TValue> dictionary)
        {
            return dictionary.Keys.Select(key => (key, dictionary[key]));
        }

        /// <summary>
        /// Finds the minimum and the maximum result from applying the <paramref name="selector"/> to the <paramref name="enumerable"/>.
        /// </summary>
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