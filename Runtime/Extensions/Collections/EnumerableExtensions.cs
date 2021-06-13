using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Foundation
{
    using UnityEditor.UIElements;

    /// <summary>
    /// Class containing extension methods for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        public static T Mode<T>(this IEnumerable<T> enumerable)
        {
            var groups       = enumerable.GroupBy(x => x);
            var biggestGroup = groups.ArgMax(group => group.Count());

            return biggestGroup.Key;
        }

        /// <summary>
        /// Execute the <paramref name="action"/> for each item in the <paramref name="enumerable"/>.
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
                action.Invoke(item);
        }

        /// <summary>
        /// Ensures the <paramref name="enumerable"/> is an array.
        /// First attempts to cast it and if that fails calls <see cref="IEnumerable{T}.ToArray()"/>
        /// </summary>
        /// <typeparam name="T">The type of elements in the <paramref name="enumerable"/>.</typeparam>
        /// <param name="enumerable">The <see cref="IEnumerable{T}"/> that we want to ensure as an array.</param>
        /// <returns>The <paramref name="enumerable"/> as array.</returns>
        public static T[] EnsureArray<T>(this IEnumerable<T> enumerable) => enumerable as T[] ?? enumerable.ToArray();

        /// <summary>
        /// Ensures the <paramref name="enumerable"/> is a <see cref="List{T}"/>.
        /// First attempts to cast it and if that fails calls <see cref="IEnumerable{T}.ToList()"/>
        /// </summary>
        /// <typeparam name="T">The type of elements in the <paramref name="enumerable"/>.</typeparam>
        /// <param name="enumerable">The <see cref="IEnumerable{T}"/> that we want to ensure as a <see cref="List{T}"/>.</param>
        /// <returns>The <paramref name="enumerable"/> as <see cref="List{T}"/>.</returns>
        public static IList<T> EnsureList<T>(this IEnumerable<T> enumerable) =>
            enumerable as IList<T> ?? enumerable.ToList();


        /// <summary>
        /// Ensures the <paramref name="enumerable"/> is a <see cref="LinkedList{T}"/>.
        /// First attempts to cast it and if that fails calls ToLinked.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <paramref name="enumerable"/>.</typeparam>
        /// <param name="enumerable">The <see cref="IEnumerable{T}"/> that we want to ensure as a <see cref="List{T}"/>.</param>
        /// <returns>The <paramref name="enumerable"/> as <see cref="List{T}"/>.</returns>
        public static LinkedList<T> EnsureLinked<T>(this IEnumerable<T> enumerable) =>
            enumerable as LinkedList<T> ?? enumerable.ToLinked();

        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Where(item => predicate.Invoke(item) == false);
        }

        /// <summary>
        /// Checks if the <paramref name="enumerable"/> is empty.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <paramref name="enumerable"/>.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>Whether the <paramref name="enumerable"/> is empty or not.</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable) => !enumerable.Any();

        public static bool ContentEquals<T>(this IReadOnlyCollection<T> collection, IReadOnlyCollection<T> other)
        {
            return collection.Count == other.Count && collection.All(other.Contains);
        }

        public static IEnumerable<TOutput> SelectWithIndex<TInput, TOutput>(this IEnumerable<TInput>   enumerable,
                                                                            Func<TInput, int, TOutput> selector)
        {
            var index = 0;
            foreach (var item in enumerable)
            {
                yield return selector.Invoke(item, index);
                index++;
            }
        }

        public static bool TryFind<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, out T result)
        {
            foreach (var item in enumerable)
            {
                if (!predicate.Invoke(item))
                    continue;

                result = item;
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Partitions the <paramref name="enumerable"/> into blocks of <paramref name="blockSize"/>.
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> enumerable, int blockSize)
        {
            IEnumerator<T> enumerator = enumerable.GetEnumerator();

            while (enumerator.MoveNext())
            {
                yield return NextPartition(blockSize);
            }

            IEnumerable<T> NextPartition(int count)
            {
                do
                {
                    yield return enumerator.Current;
                } while (--count > 0 && enumerator.MoveNext());
            }
        }

        public static IEnumerable<TTarget> SelectNotNull<TSource, TTarget>(
            this IEnumerable<TSource> source, Func<TSource, TTarget> selector)
        {
            return source.Select(selector.Invoke).Where(target => target != null);
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> enumerable, Func<T, bool> last)
        {
            var enumerator = enumerable.GetEnumerator();

            while (enumerator.MoveNext())
            {
                yield return NextPartition();
            }

            IEnumerable<T> NextPartition()
            {
                T current;
                do
                {
                    current = enumerator.Current;
                    yield return current;
                } while (!last(current) && enumerator.MoveNext());
            }
        }

        /// <summary>
        /// Finds the element in the <paramref name="enumerable"/> that scores the best with the <paramref name="scoreFunction"/>.
        /// </summary> 
        public static T ArgMax<T>(this IEnumerable<T> enumerable, Func<T, float> scoreFunction)
        {
            T[] array = enumerable.EnsureArray();
            int index = array.IndexOfBest(scoreFunction);

            return index >= 0 ? array[index] : default;
        }

        public static T Best<T>(this IEnumerable<T> enumerable, Func<T, int> scoreFunction)
        {
            T[] array = enumerable.EnsureArray();
            int index = array.IndexOfBest(scoreFunction);

            return index >= 0 ? array[index] : default;
        }

        /// <summary>
        /// Finds the element in the <paramref name="enumerable"/> that scores the worst with the <paramref name="scoreFunction"/>.
        /// </summary> 
        public static T ArgMin<T>(this IEnumerable<T> enumerable, Func<T, float> scoreFunction)
        {
            T[] array = enumerable.EnsureArray();
            int index = array.IndexOfWorst(scoreFunction);

            return index >= 0 ? array[index] : default;
        }

        /// <summary>
        /// Returns true if only a single element in the <paramref name="enumerable"/> satisfies the <paramref name="predicate"/>.
        /// If this is the case, will set output that through <paramref name="output"/>.
        /// </summary>
        public static bool GetIfSingle<T>(this IEnumerable<T> enumerable, Predicate<T> predicate, out T output)
        {
            T[] array = enumerable.EnsureArray();

            bool success = array.GetIndexIfSingle(predicate, out int index);
            output = success ? array[index] : default;

            return success;
        }


        /// <summary>
        /// Finds the index of the element in the <paramref name="enumerable"/> that scores the best with the <paramref name="scoreFunction"/>.
        /// </summary> 
        public static int IndexOfBest<T>(this IEnumerable<T> enumerable, Func<T, float> scoreFunction)
        {
            //Cast to list so we only enumerate once and can use indexing.
            T[] array = enumerable.EnsureArray();

            //Ensure the list is filled.
            if (array.IsEmpty())
                return -1;

            //Start with first element.
            int   bestIndex = 0;
            float bestScore = scoreFunction(array[bestIndex]);

            //Find best scoring element.
            for (int index = 1; index < array.Length; index++)
            {
                //Get element and score.
                T     element = array[index];
                float score   = scoreFunction(element);

                //Compare and set.
                if (score <= bestScore)
                    continue;
                bestIndex = index;
                bestScore = score;
            }

            return bestIndex;
        }

        /// <summary>
        /// Finds the index of the element in the <paramref name="enumerable"/> that scores the best with the <paramref name="scoreFunction"/>.
        /// </summary> 
        public static int IndexOfBest<T>(this IEnumerable<T> enumerable, Func<T, int> scoreFunction)
        {
            //Cast to list so we only enumerate once and can use indexing.
            T[] array = enumerable.EnsureArray();

            //Ensure the list is filled.
            if (array.IsEmpty())
                return -1;

            //Start with first element.
            int   bestIndex = 0;
            float bestScore = scoreFunction(array[bestIndex]);

            //Find best scoring element.
            for (int index = 1; index < array.Length; index++)
            {
                //Get element and score.
                T     element = array[index];
                float score   = scoreFunction(element);

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
            T[] array = enumerable.EnsureArray();

            //Ensure the list is filled.
            if (array.IsEmpty())
                return -1;

            //Start with first element.
            int   worstIndex = 0;
            float worstScore = scoreFunction(array[worstIndex]);

            //Find best scoring element.
            for (int index = 1; index < array.Length; index++)
            {
                //Get element and score.
                T     element = array[index];
                float score   = scoreFunction(element);

                //Compare and set.
                if (score >= worstScore)
                    continue;
                worstIndex = index;
                worstScore = score;
            }

            return worstIndex;
        }

        /// <summary>
        /// Returns true if only a single element in the <paramref name="enumerable"/> satisfies the <paramref name="predicate"/>.
        /// If this is the case, will output the index of that element through <paramref name="index"/>.
        /// </summary>
        public static bool GetIndexIfSingle<T>(this IEnumerable<T> enumerable, Predicate<T> predicate, out int index)
        {
            index = -1;
            T[] array = enumerable.EnsureArray();

            for (int i = 0; i < array.Length; i++)
            {
                T item = array[i];

                if (!predicate(item))
                    continue;

                if (index == -1)
                {
                    index = i;
                }
                else
                {
                    index = -1;
                    return false;
                }
            }

            return true;
        }

        public static int GetIndexIfSingle<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            var index          = 0;
            var predicateIndex = -1;

            foreach (var item in enumerable)
            {
                if (predicate.Invoke(item))
                {
                    if (predicateIndex == -1)
                        predicateIndex = index;
                    else
                        return -1;
                }

                index++;
            }

            return predicateIndex;
        }

        public static IEnumerable<T> Cumulative<T>(this IEnumerable<T> enumerable, Func<T, T, T> aggregator)
        {
            IEnumerator<T> enumerator = enumerable.GetEnumerator();

            T aggregation = enumerator.Current;
            yield return aggregation;

            while (enumerator.MoveNext())
            {
                T item = enumerator.Current;
                aggregation = aggregator(aggregation, item);

                yield return aggregation;
            }

            enumerator.Dispose();
        }

        /// <summary>
        /// Checks if the <paramref name="enumerable"/> range contains the <paramref name="index"/>.
        /// </summary>
        public static bool ContainsIndex<T>(this IEnumerable<T> enumerable, int index) =>
            0 <= index && index < enumerable.Count();


        /// <summary>
        /// Creates a <see cref="LinkedList{T}"/> from the <paramref name="enumerable"/>.
        /// </summary>
        public static LinkedList<T> ToLinked<T>(this IEnumerable<T> enumerable)
        {
            var linkedList = new LinkedList<T>();
            foreach (var element in enumerable)
            {
                linkedList.AddLast(element);
            }

            return linkedList;
        }

        public static BucketSet<TKey, TValue> ToBuckets<TKey, TValue>(this IEnumerable<TValue> enumerable,
                                                                      Func<TValue, TKey>       bucketSelector)
        {
            return new BucketSet<TKey, TValue>(enumerable, bucketSelector);
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

        public static IEnumerable<T> DropEndWhile<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) =>
            enumerable.ApplyBackwards(sequence => sequence.SkipWhile(predicate));

        public static IEnumerable<T> ApplyBackwards<T>(this IEnumerable<T>                  enumerable,
                                                       Func<IEnumerable<T>, IEnumerable<T>> projection)
        {
            IEnumerable<T> reverse = enumerable.Reverse();
            IEnumerable<T> applied = projection(reverse);
            return applied.Reverse();
        }

        public static int Product(this IEnumerable<int> items) => items.Aggregate(1, (x, y) => x * y);

        public static int GCD(this IEnumerable<int> numbers) => numbers.Aggregate(MathHelper.GCD);

        public static int LCM(this IEnumerable<int> numbers) => numbers.Aggregate(1, MathHelper.LCM);

        public static IEnumerable<T> AsEnumerable<T>(this (T x, T y) tuple)
        {
            yield return tuple.x;
            yield return tuple.y;
        }

        public static void Enumerate<T>(this IEnumerable<T> enumerable)
        {
            var _ = enumerable.ToList();
        }
    }
}