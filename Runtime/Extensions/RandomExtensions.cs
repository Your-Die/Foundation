using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada
{
    /// <summary>
    /// Class that adds extension methods for IEnumerable for choosing and taking random elements.
    /// </summary>
    public static class RandomExtensions
    {
        private static UnityRandom uRandom = new UnityRandom();
        
        #region Indices

        public static IEnumerable<int> RandomIndices<T>(this IList<T> list, IRNG random = null)
        {
            var maxIndex = list.Count;
            random ??= uRandom;

            while (true)
                yield return random.Range(maxIndex);
        }
        
        
        public static IEnumerable<int> RandomIndicesDistinct<T>(this IList<T> list, IRNG random = null)
        {
            var indices = list.GetIndices().ToList();

            while (indices.Any())
                yield return indices.GrabRandom(random);
        }

        /// <summary>
        /// Chooses a random valid index for the <paramref name="list"/>.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the enumerable.</typeparam>
        /// <param name="list">The enumerable to choose an index for.</param>
        /// <param name="random">Random number generator.</param>
        /// <returns>A valid index for the enumerable.</returns>
        public static int ChooseRandomIndex<T>(this IList<T> list, IRNG random = null)
        {
            var indexMax = list.Count;
            random ??= uRandom;
            
            return list.Count > 0 ? random.Range(indexMax) : -1;
        }

        /// <summary>
        /// Chooses multiple random indices.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to choose indices for.</param>
        /// <param name="amount">The amount of indices to choose.</param>
        /// <param name="random">Random number generator.</param>
        /// <remarks>The same index may be chosen multiple times. If Distinct indices are needed, use <see cref="ChooseRandomIndicesDistinct{T}"/>.</remarks>
        /// <returns>The randomly chosen indices.</returns>
        public static IEnumerable<int> ChooseRandomIndices<T>(this IEnumerable<T> enumerable, int amount, IRNG random = null)
        {
            var list = enumerable.EnsureList();
            var randomIndices = list.RandomIndices(random);

            return randomIndices.Take(amount);
        }

        /// <summary>
        /// Chooses multiple random distinct indices.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to choose indices for.</param>
        /// <param name="amount">The amount of indices to choose.</param>
        /// <param name="random">Random number generator.</param>
        /// <returns>Multiple random distinct indices.</returns>
        public static IEnumerable<int> ChooseRandomIndicesDistinct<T>(this IEnumerable<T> enumerable, int amount, IRNG random = null)
        {
            var list = enumerable.EnsureList();
            return list.RandomIndicesDistinct(random).Take(amount);
        }

        public static IList<int> GetIndicesShuffled<T>(this IList<T> list, IRNG random = null)
        {
            var indices = list.GetIndices().ToList();
            indices.Shuffle(random);

            return indices;
        }

        #endregion
        
        #region Choosing

        public static T ChooseRandom<T>(this IEnumerable<T> enumerable, IRNG random = null)
        {
            var list = enumerable.EnsureList();
            return list.ChooseRandom(random);
        }
        
        /// <summary>
        /// Chooses a random element.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the enumerable.</typeparam>
        /// <param name="list">The enumerable to choose an element of.</param>
        /// <param name="random">Random number generator.</param>
        /// <returns>A randomly chosen element.</returns>
        public static T ChooseRandom<T>(this IList<T> list, IRNG random = null)
        {
            if (list.IsEmpty())
                return default;

            var index = list.ChooseRandomIndex(random);
            return list[index];
        }

        public static IEnumerable<T> RandomElements<T>(this IList<T> list, IRNG random = null)
        {
            var indices = list.RandomIndices(random);
            return indices.Select(index => list[index]);
        }
        public static IEnumerable<T> RandomOrder<T>(this IEnumerable<T> enumerable, IRNG random = null)
        {
            var list = enumerable.EnsureList();
            
            var indices = list.RandomIndicesDistinct(random);
            return indices.Select(index => list[index]);
        }

        /// <summary>
        /// Chooses multiple random elements.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to choose an element of.</param>
        /// <param name="amount">The amount of elements to choose.</param>
        /// <remarks>The same element may be chosen multiple times. If Distinct elements are needed, use <see cref="ChooseRandomDistinct{T}"/>.</remarks>
        /// <returns>The randomly chosen elements.</returns>
        public static IEnumerable<T> ChooseRandom<T>(this IList<T> enumerable, int amount, IRNG random = null)
        {
            return enumerable.RandomElements(random).Take(amount);
        }

        /// <summary>
        /// Randomly chooses multiple distinct elements.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the enumerable.</typeparam>
        /// <param name="list">The enumerable to choose an element of.</param>
        /// <param name="amount">The amount of elements to choose.</param>
        /// <returns>Multiple randomly chosen elements.</returns>
        public static IEnumerable<T> ChooseRandomDistinct<T>(this IEnumerable<T> list, int amount, IRNG random = null)
        {
            return list.RandomOrder(random).Take(amount);
        }

        #region Weighted

        /// <summary>
        /// Chooses a random element from the <paramref name="enumerable"/> where the weights as determined by the <paramref name="weightFunction"/> bias the selection.
        /// </summary> 
        public static T ChooseRandomWeighted<T>(this IEnumerable<T> enumerable, Func<T, float> weightFunction, IRNG random = null)
        {
            var weightedCollection = new Dictionary<T, float>();

            foreach (var element in enumerable)
            {
                var weight = weightFunction(element);
                weightedCollection.Add(element, weight);
            }

            return weightedCollection.ChooseRandomWeighted(random).First();
        }

        public static IEnumerable<T> ChooseRandomWeighted<T>(this IDictionary<T, float> weightedCollection, IRNG random = null)
        {
            var weightSum = weightedCollection.Values.Sum();
            random ??= UnityRandom.Shared;

            while (true)
            {
                var randomValue = random.Range(weightSum);
                var items       = weightedCollection.Keys.RandomOrder(random);
                
                foreach (var item in items)
                {
                    var weight = weightedCollection[item];
                    randomValue -= weight;

                    if (randomValue <= 0)
                    {
                        yield return item;
                        break;
                    }
                }
            }
        }

        #endregion

        #region Preferred

        /// <summary>
        /// Chooses a random element, where elements that satisfy the <paramref name="predicate"/> are given precedence if they are 
        /// present in the <paramref name="list"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
        /// <param name="list">The list to choose elements of.</param>
        /// <param name="predicate">The predicate that preferred elements satisfy.</param>
        /// <returns>A randomly chosen element if any is present, or a completely randomly chosen element otherwise.</returns>
        public static T ChooseRandomPreferred<T>(this IList<T> list, Func<T, bool> predicate, IRNG random = null)
        {
            //Cast to arrays to avoid multiple enumerations.
            var preferredElements = list.Where(predicate).ToArray();

            //Choose a preferred elements if there are any, otherwise any other possible element.
            return preferredElements.Any()
                ? preferredElements.ChooseRandom(random)
                : list.ChooseRandom(random);
        }

        /// <summary>
        /// Chooses multiple distinct elements, where elements that satisfy the <paramref name="predicate"/> are preferred.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="enumerable">The enumerable to choose elements of.</param>
        /// <param name="predicate">The predicate that preferred elements satisfy.</param>
        /// <param name="amount">The amount of elements to choose.</param>
        /// <returns>Randomly chosen elements if any are present, plus additional completely random chosen element otherwise.</returns>
        public static IEnumerable<T> ChooseRandomPreferred<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, int amount, IRNG random = null)
        {
            return enumerable.RandomElementsPreferred(predicate, random).Take(amount);
        }

        public static IEnumerable<T> RandomElementsPreferred<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, IRNG random = null)
        {
            var preferred = new List<T>();
            var unpreferred = new List<T>();

            foreach (var item in enumerable)
            {
                if (predicate(item)) 
                    preferred.Add(item);
                else
                    unpreferred.Add(item);
            }

            foreach (var item in  preferred.RandomOrder(random))
                yield return item;

            foreach (var item in unpreferred.RandomOrder(random))
                yield return item;
        }

        #endregion


        #endregion

        #region Grabbing

        public static IEnumerable<T> GrabRandomElements<T>(this IList<T> list, IRNG random = null)
        {
            while (list.Any())
                yield return list.GrabRandom(random);
        }
        
        /// <summary>
        /// Chooses and removes a random element of the <paramref name="list"/>.
        /// </summary>
        /// <typeparam name="T">Type of elements in the list.</typeparam>
        /// <param name="list">The list to choose elements from.</param>
        /// <returns>The randomly grabbed element.</returns>
        public static T GrabRandom<T>(this IList<T> list, IRNG random = null)
        {
            var index = list.ChooseRandomIndex(random);
            return list.Extract(index);
        }

        /// <summary>
        /// Randomly chooses and removes multiple elements from the <paramref name="list"/>.
        /// </summary>
        /// <typeparam name="T">Type of elements in the list.</typeparam>
        /// <param name="list">The list to choose elements from.</param>
        /// <param name="amount">Amount of elements to grab.</param>
        /// <returns>The grabbed elements.</returns>
        public static IEnumerable<T> GrabRandom<T>(this IList<T> list, int amount, IRNG random = null)
        {
            return list.GrabRandomElements(random).Take(amount);
        }

        #endregion

        /// <summary>
        /// Shuffle the <paramref name="list"/> in place.
        /// </summary>
        public static void Shuffle<T>(this IList<T> list, IRNG random = null)
        {
            random ??= uRandom;
            
            var unshuffled = list.Count;
            while (unshuffled > 1)
            {
                unshuffled--;
                var randomIndex = random.Range(unshuffled + 1);
                var value       = list[randomIndex];
                
                list[randomIndex] = list[unshuffled];
                list[unshuffled] = value;
            }
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable, IRNG random = null)
        {
            var buffer = enumerable.ToList();
            random ??= uRandom;

            var count = buffer.Count;
            for (var index = 0; index < count; index++)
            {
                var randomIndex = random.Range(index, count);
                yield return buffer[randomIndex];

                buffer[randomIndex] = buffer[index];
            }
        }

        public static Vector2 RandomInRect(this Rect rect, IRNG random = null)
        {
            random ??= uRandom;
            
            var x = random.Range(rect.xMin, rect.xMax);
            var y = random.Range(rect.yMin, rect.yMax);
            
            return new Vector2(x, y);
        }
    }
}
