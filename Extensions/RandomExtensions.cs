using System;
using System.Collections.Generic;
using System.Linq;
using Random = Chinchillada.Utilities.Random;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Class that adds extension methods for IEnumerable for choosing and taking random elements.
    /// </summary>
    public static class RandomExtensions
    {
        #region Choosing

        /// <summary>
        /// Chooses a random element.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to choose an element of.</param>
        /// <returns>A randomly chosen element.</returns>
        public static T ChooseRandom<T>(this IEnumerable<T> enumerable)
        {
            IEnumerable<T> array = enumerable.EnsureArray();

            int index = array.ChooseRandomIndex();
            return array.ElementAt(index);
        }

        /// <summary>
        /// Chooses multiple random elements.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to choose an element of.</param>
        /// <param name="amount">The amount of elements to choose.</param>
        /// <remarks>The same element may be chosen multiple times. If Distinct elements are needed, use <see cref="ChooseRandomDistinct{T}"/>.</remarks>
        /// <returns>The randomly chosen elements.</returns>
        public static IEnumerable<T> ChooseRandom<T>(this IEnumerable<T> enumerable, int amount)
        {
            Func<T> chooseRandom = enumerable.ChooseRandom;
            return chooseRandom.Generate(amount);
        }

        /// <summary>
        /// Randomly chooses multiple distinct elements.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to choose an element of.</param>
        /// <param name="amount">The amount of elements to choose.</param>
        /// <remarks>If the <paramref name="amount"/> is more than the total amount of elements in <paramref name="enumerable"/>, the <paramref name="enumerable"/> is returned as is.</remarks>
        /// <returns>Multiple randomly chosen elements.</returns>
        public static IEnumerable<T> ChooseRandomDistinct<T>(this IEnumerable<T> enumerable, int amount)
        {
            var list = enumerable.ToList();
            return list.Count <= amount ? list : list.GrabRandom(amount);
        }

        #region Weighted

        /// <summary>
        /// Chooses a random element from the <paramref name="enumerable"/> where the weights as determined by the <paramref name="weightFunction"/> bias the selection.
        /// </summary> 
        public static T ChooseRandomWeighted<T>(this IEnumerable<T> enumerable, Func<T, float> weightFunction, bool shuffle = false)
        {
            var weightedCollection = new List<(T, float)>();

            foreach (T element in enumerable)
            {
                float weight = weightFunction(element);
                var weightedElement = (element, weight);

                weightedCollection.Add(weightedElement);
            }

            return ChooseRandomWeighted(weightedCollection, shuffle);
        }

        /// <summary>
        /// Chooses a random element from the <see cref="weightedCollection"/> using the weights as bias while making the choice.
        /// </summary>
        /// <param name="weightedCollection">The collection we're choosing from.</param>
        /// <param name="shuffle">Whether to shuffle the collection beforehand.</param>
        public static T ChooseRandomWeighted<T>(IList<(T, float)> weightedCollection, bool shuffle = false)
        {
            if (shuffle)
            {
                weightedCollection.Shuffle();
            }

            //Sum the weights.
            float weightSum = weightedCollection.Sum(tuple => tuple.Item2);

            //Generate a value lower than the sum.
            float randomValue = Random.Range(weightSum);

            foreach ((T element, float weight) in weightedCollection)
            {
                randomValue -= weight;

                if (randomValue <= 0)
                    return element;
            }

            return weightedCollection.Last().Item1;
        }

        #endregion

        #region Where
        
        /// <summary>
        /// Randomly chooses multiple elements that satisfy the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <paramref name="enumerable"/>.</typeparam>
        /// <param name="enumerable">The enumerable to choose elements of.</param>
        /// <param name="predicate">The predicate the randomly chosen element should satisfy.</param>
        /// <param name="amount">The amount of random elements to select.</param>
        /// <returns>Multiple randomly chosen elements of <paramref name="enumerable"/> that satisfy the <paramref name="predicate"/>.</returns>
        public static IEnumerable<T> ChooseRandomWhere<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, int amount)
        {
            IEnumerable<T> validElements = enumerable.Where(predicate);
            return validElements.ChooseRandom(amount);
        }

        /// <summary>
        /// Randomly chooses multiple distinct elements that satisfy the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to choose an element of.</param>
        /// <param name="predicate">The predicate that the chosen elements need to satisfy.</param>
        /// <param name="amount">The amount of elements to choose.</param>
        /// <returns>Multiple randomly chosen elements.</returns>
        public static IEnumerable<T> ChooseRandomDistinctWhere<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, int amount)
        {
            IEnumerable<T> validElements = enumerable.Where(predicate);
            return validElements.ChooseRandomDistinct(amount);
        }

        #endregion

        #region Preferred

        /// <summary>
        /// Chooses a random element, where elements that satisfy the <paramref name="predicate"/> are given precedence if they are 
        /// present in the <paramref name="enumerable"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to choose elements of.</param>
        /// <param name="predicate">The predicate that preferred elements satisfy.</param>
        /// <returns>A randomly chosen element if any is present, or a completely randomly chosen element otherwise.</returns>
        public static T ChooseRandomPreferred<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            //Cast to arrays to avoid multiple enumerations.
            IEnumerable<T> asArray = enumerable.EnsureArray();
            IEnumerable<T> preferredElements = asArray.Where(predicate).ToArray();

            //Choose a preferred elements if there are any, otherwise any other possible element.
            return preferredElements.Any()
                ? preferredElements.ChooseRandom()
                : asArray.ChooseRandom();
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
            //Select preferred elements.
            enumerable = enumerable.ToArray();
            IEnumerable<T> preferredSelection = enumerable.ChooseRandomDistinctWhere(predicate, amount).ToArray();

            //If we have enough, return selection.
            int selectionCount = preferredSelection.Count();
            if (selectionCount == amount)
                return preferredSelection;

            //Select the remaining amount from the rest of the elements.
            int amountLeft = amount - selectionCount;
            IEnumerable<T> unpreferredSelection = enumerable.ChooseRandomDistinctWhere(x => !predicate(x), amountLeft);

            //Concat the two selections.
            return preferredSelection.Concat(unpreferredSelection);
        }

        #endregion

        #region Indices

        /// <summary>
        /// Chooses a random valid index for the <paramref name="enumerable"/>.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to choose an index for.</param>
        /// <returns>A valid index for the enumerable.</returns>
        public static int ChooseRandomIndex<T>(this IEnumerable<T> enumerable)
        {
            int indexMax = enumerable.Count();
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
            int indexMax = enumerable.Count();
            Func<int> generationFunction = () => Random.Range(indexMax);

            return generationFunction.Generate(amount);
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
            int indexMax = enumerable.Count();
            var indices = Enumerable.Range(0, indexMax);

            return indices.ChooseRandomDistinct(amount);
        }

        #endregion

        #endregion

        #region Grabbing

        /// <summary>
        /// Chooses and removes a random element of the <paramref name="list"/>.
        /// </summary>
        /// <typeparam name="T">Type of elements in the list.</typeparam>
        /// <param name="list">The list to choose elements from.</param>
        /// <returns>The randomly grabbed element.</returns>
        public static T GrabRandom<T>(this IList<T> list)
        {
            int index = list.ChooseRandomIndex();
            T element = list.ElementAt(index);
            
            list.RemoveAt(index);
            return element;
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
            Func<T> grabRandom = list.GrabRandom;
            return grabRandom.Generate(amount);
        }

        #endregion

        /// <summary>
        /// Shuffle the <paramref name="list"/> in place.
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            int unshuffled = list.Count;
            while (unshuffled > 1)
            {
                unshuffled--;
                int random = Random.Range(unshuffled + 1);
                T value = list[random];
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
