﻿namespace Chinchillada
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Schema;
    using Chinchillada;
    using UnityEngine;

    public interface IRNG
    {
        void SetSeed(int seed);
        float Float();
        float Range(float min, float max);
        int Range(int     min, int   max, bool inclusive = false);
    }

    public static class RNGExtensions
    {
        public static int Int(this IRNG rng) => rng.Range(int.MinValue, int.MaxValue);

        public static IEnumerable<T> Repeat<T>(this IRNG rng, Func<IRNG, T> generator)
        {
            while (true)
                yield return generator.Invoke(rng);
        }

        public static int RandomInRange(this RangeInt range, IRNG rng)
        {
            return rng.Range(range.Minimum, range.Maximum);
        }
        
        public static float RandomInRange(this Range range, IRNG rng)
        {
            return rng.Range(range.Minimum, range.Maximum);
        }
        
        public static int Range(this IRNG rng, int max, bool inclusive = false)
        {
            return rng.Range(0, max, inclusive);
        }

        public static float Range(this IRNG rng, float max)
        {
            rng ??= UnityRandom.Shared;
            return rng.Range(0, max);
        }

        public static Vector2 Direction(this IRNG rng)
        {
            var vector = new Vector2
            {
                x = rng.Float(),
                y = rng.Float()
            };

            return vector.normalized;
        }

        public static bool Flip(this IRNG rng, float probability = 0.5f)
        {
            return rng.Float() < probability;
        }

        public static T Choose<T>(this IRNG rng, T left, T right, float probability = 0.5f)
        {
            return rng.Flip(probability) ? left : right;
        }

        public static T Choose<T>(this IRNG rng, params T[] items) => items.ChooseRandom(rng);

        public static float Range(this IRNG rng, Vector2 range) => rng.Range(range.x, range.y);

        public static T Choose<T>(this IRNG rng) where T : Enum
        {
            return EnumHelper.GetValues<T>().ChooseRandom(rng);
        }

        public static Vector2Int Vector2Int(this IRNG rng, int xMin, int xMax, int yMin, int yMax)
        {
            return new Vector2Int
            {
                x = rng.Range(xMin, xMax),
                y = rng.Range(yMin, yMax)
            };
        }

        public static Vector2 InCircle(this IRNG random, Vector2 center, float radius)
        {
            var x = random.Range(-1f, 1f);
            var y = random.Range(-1f, 1f);

            var direction = new Vector2(x, y).normalized;
            var delta     = direction * random.Range(radius);

            return center + delta;
        }

        public static T ChooseAndExtract<T>(this IRNG random, IList<T> list)
        {
            var index = random.ChooseIndex(list);
            var item  = list[index];

            list.RemoveAt(index);

            return item;
        }

        /// <summary>
        /// Shuffles the <paramref name="items"/>,
        /// with a higher chance for items that score high through the <paramref name="weightFunction"/>.
        /// </summary>
        /// <remarks>
        /// Adapted from https://utopia.duth.gr/~pefraimi/research/data/2007EncOfAlg.pdf
        /// </remarks>
        public static IEnumerable<T> ShuffleWeighted<T>(this IRNG      random,
                                                        IEnumerable<T> items,
                                                        Func<T, float> weightFunction)
        {
            return items.OrderByDescending(GenerateKey);

            float GenerateKey(T item)
            {
                var value  = random.Float();
                var weight = weightFunction.Invoke(item);

                return Mathf.Pow(value, 1f / weight);
            }
        }

        public static IEnumerable<T> ChooseWeighted<T>(this IRNG      random,
                                                       IEnumerable<T> items,
                                                       Func<T, float> weightFunction,
                                                       int            amount)
        {
            return random.ShuffleWeighted(items, weightFunction).Take(amount);
        }

        public static T ChooseWeighted<T>(this IRNG      random,
                                          IEnumerable<T> items,
                                          Func<T, float> weightFunction)
        {
            return ShuffleWeighted(random, items, weightFunction).First();
        }

        public static T Choose<T>(this IRNG random, IReadOnlyList<T> list)
        {
            var index = random.ChooseIndex(list);
            return list[index];
        }

        public static T Choose<T>(this IRNG random, IReadOnlyCollection<T> items)
        {
            var index = random.ChooseIndex(items);
            return items.ElementAt(index);
        }

        public static int ChooseIndex<T>(this IRNG random, IList<T>               list) => random.Range(0, list.Count);
        public static int ChooseIndex<T>(this IRNG random, IReadOnlyCollection<T> list) => random.Range(0, list.Count);
    }
}