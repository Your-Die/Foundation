using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// Class that adds extension methods for IEnumerable for choosing and taking random elements.
    /// </summary>
    public static class RandomExtensions
    {
        
        #region Indices

        public static IEnumerable<int> RandomIndices<T>(this IList<T> list)
        {
            var maxIndex = list.Count;

            while (true)
                yield return Random.Range(maxIndex);
        }
        
        
        public static IEnumerable<int> RandomIndicesDistinct<T>(this IList<T> list)
        {
            var indices = list.GetIndices().ToList();

            while (indices.Any())
                yield return indices.GrabRandom();
        }

        /// <summary>
        /// Chooses a random valid index for the <paramref name="enumerable"/>.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to choose an index for.</param>
        /// <returns>A valid index for the enumerable.</returns>
        public static int ChooseRandomIndex<T>(this IList<T> enumerable)
        {
            int indexMax = enumerable.Count;
            return Random.Range(indexMax);
        }

        /// <summary>
        /// Chooses multiple random indices.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to choose indices for.</param>
        /// <param name="amount">The amount of indices to choose.</param>
        /// <remarks>The same index may be chosen multiple times. If Distinct indices are needed, use <see cref="ChooseRandomIndicesDistinct{T}"/>.</remarks>
        /// <returns>The randomly chosen indices.</returns>
        public static IEnumerable<int> ChooseRandomIndices<T>(this IEnumerable<T> enumerable, int amount)
        {
            var list = enumerable.EnsureList();
            var randomIndices = list.RandomIndices();

            return randomIndices.Take(amount);
        }

        /// <summary>
        /// Chooses multiple random distinct indices.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to choose indices for.</param>
        /// <param name="amount">The amount of indices to choose.</param>
        /// <returns>Multiple random distinct indices.</returns>
        public static IEnumerable<int> ChooseRandomIndicesDistinct<T>(this IEnumerable<T> enumerable, int amount)
        {
            var list = enumerable.EnsureList();
            return list.RandomIndicesDistinct().Take(amount);
        }

        public static IList<int> GetIndicesShuffled<T>(this IList<T> list)
        {
            var indices = list.GetIndices().ToList();
            indices.Shuffle();

            return indices;
        }

        #endregion
        
        #region Choosing

        public static T ChooseRandom<T>(this IEnumerable<T> enumerable)
        {
            var list = enumerable.EnsureList();
            return list.ChooseRandom();
        }
        
        /// <summary>
        /// Chooses a random element.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the enumerable.</typeparam>
        /// <param name="list">The enumerable to choose an element of.</param>
        /// <returns>A randomly chosen element.</returns>
        public static T ChooseRandom<T>(this IList<T> list)
        {
            var index = list.ChooseRandomIndex();
            return list[index];
        }

        public static IEnumerable<T> RandomElements<T>(this IList<T> list)
        {
            var indices = list.RandomIndices();
            return indices.Select(index => list[index]);
        }
        public static IEnumerable<T> RandomOrder<T>(this IEnumerable<T> enumerable)
        {
            var list = enumerable.EnsureList();
            
            var indices = list.RandomIndicesDistinct();
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
        public static IEnumerable<T> ChooseRandom<T>(this IList<T> enumerable, int amount)
        {
            return enumerable.RandomElements().Take(amount);
        }

        /// <summary>
        /// Randomly chooses multiple distinct elements.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the enumerable.</typeparam>
        /// <param name="list">The enumerable to choose an element of.</param>
        /// <param name="amount">The amount of elements to choose.</param>
        /// <returns>Multiple randomly chosen elements.</returns>
        public static IEnumerable<T> ChooseRandomDistinct<T>(this IEnumerable<T> list, int amount)
        {
            return list.RandomOrder().Take(amount);
        }

        #region Weighted

        /// <summary>
        /// Chooses a random element from the <paramref name="enumerable"/> where the weights as determined by the <paramref name="weightFunction"/> bias the selection.
        /// </summary> 
        public static T ChooseRandomWeighted<T>(this IEnumerable<T> enumerable, Func<T, float> weightFunction)
        {
            var weightedCollection = new Dictionary<T, float>();

            foreach (var element in enumerable)
            {
                var weight = weightFunction(element);
                weightedCollection.Add(element, weight);
            }

            return weightedCollection.ChooseRandomWeighted().First();
        }

        public static IEnumerable<T> ChooseRandomWeighted<T>(this IDictionary<T, float> weightedCollection)
        {
            var weightSum = weightedCollection.Values.Sum();

            while (true)
            {
                var randomValue = Random.Range(weightSum);
                var items = weightedCollection.Keys.RandomOrder();
                
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
        public static T ChooseRandomPreferred<T>(this IList<T> list, Func<T, bool> predicate)
        {
            //Cast to arrays to avoid multiple enumerations.
            var preferredElements = list.Where(predicate).ToArray();

            //Choose a preferred elements if there are any, otherwise any other possible element.
            return preferredElements.Any()
                ? preferredElements.ChooseRandom()
                : list.ChooseRandom();
        }

        /// <summary>
        /// Chooses multiple distinct elements, where elements that satisfy the <paramref name="predicate"/> are preferred.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="enumerable">The enumerable to choose elements of.</param>
        /// <param name="predicate">The predicate that preferred elements satisfy.</param>
        /// <param name="amount">The amount of elements to choose.</param>
        /// <returns>Randomly chosen elements if any are present, plus additional completely random chosen element otherwise.</returns>
        public static IEnumerable<T> ChooseRandomPreferred<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, int amount)
        {
            return enumerable.RandomElementsPreferred(predicate).Take(amount);
        }

        public static IEnumerable<T> RandomElementsPreferred<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
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

            foreach (var item in  preferred.RandomOrder())
                yield return item;

            foreach (var item in unpreferred.RandomOrder())
                yield return item;
        }

        #endregion


        #endregion

        #region Grabbing

        public static IEnumerable<T> GrabRandomElements<T>(this IList<T> list)
        {
            while (list.Any())
                yield return list.GrabRandom();
        }
        
        /// <summary>
        /// Chooses and removes a random element of the <paramref name="list"/>.
        /// </summary>
        /// <typeparam name="T">Type of elements in the list.</typeparam>
        /// <param name="list">The list to choose elements from.</param>
        /// <returns>The randomly grabbed element.</returns>
        public static T GrabRandom<T>(this IList<T> list)
        {
            var index = list.ChooseRandomIndex();
            return list.Extract(index);
        }

        /// <summary>
        /// Randomly chooses and removes multiple elements from the <paramref name="list"/>.
        /// </summary>
        /// <typeparam name="T">Type of elements in the list.</typeparam>
        /// <param name="list">The list to choose elements from.</param>
        /// <param name="amount">Amount of elements to grab.</param>
        /// <returns>The grabbed elements.</returns>
        public static IEnumerable<T> GrabRandom<T>(this IList<T> list, int amount)
        {
            return list.GrabRandomElements().Take(amount);
        }

        #endregion

        /// <summary>
        /// Shuffle the <paramref name="list"/> in place.
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            var unshuffled = list.Count;
            while (unshuffled > 1)
            {
                unshuffled--;
                var random = Random.Range(unshuffled + 1);
                var value = list[random];
                
                list[random] = list[unshuffled];
                list[unshuffled] = value;
            }
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            var buffer = enumerable.ToList();

            int count = buffer.Count;
            for (int index = 0; index < count; index++)
            {
                int randomIndex = Random.Range(index, count);
                yield return buffer[randomIndex];

                buffer[randomIndex] = buffer[index];
            }
        }
    }
}
