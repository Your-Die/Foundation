namespace Chinchillada
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Schema;
    using Chinchillada;
    using UnityEngine;

    public interface IRNG : IInitializable
    {
        void SetSeed(int seed);
        float Float();
        float Range(float min,     float max);
        int Range(int     min,     int   max, bool inclusive = false);
    }

    public static class RNGExtensions
    {
        public static IEnumerable<T> Repeat<T>(this IRNG rng, Func<IRNG, T> generator)
        {
            while (true)
                yield return generator.Invoke(rng);
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
    }
}