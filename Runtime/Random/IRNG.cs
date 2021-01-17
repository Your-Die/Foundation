namespace Chinchillada
{
    using System;
    using Foundation;
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
        public static int Range(this IRNG rng, int max, bool inclusive = false)
        {
            return rng.Range(0, max, inclusive);
        }

        public static float Range(this IRNG rng, float max) => rng.Range(0, max);

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
    }
}