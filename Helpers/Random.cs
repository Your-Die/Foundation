using System;
using System.Text;
using UnityEngine;
using RNG = UnityEngine.Random;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Wrapper class for <see cref="UnityEngine.Random"/> that also adds some common overloads.
    /// </summary>
    public static class Random
    {
        /// <summary>
        /// Generates a random <see cref="float"/> value between 0 and 1.
        /// </summary>
        public static float Value => RNG.value;

        /// <summary>
        /// Generate a random <see cref="float"/> value between 0 and 1.
        /// </summary>
        public static float value => RNG.value;

        public static void SetSeed(int seed) => RNG.InitState(seed);
        
        /// <summary>
        /// Generate a random <see cref="int"/>  between 0 and <paramref name="max"/>.
        /// </summary>
        public static int Range(int max)
        {
            return Range(0, max);
        }

        /// <summary>
        /// Generate a random <see cref="int"/> between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        public static int Range(int min, int max)
        {
            return RNG.Range(min, max);
        }

        /// <summary>
        /// Generate a random <see cref="float"/>  between 0 and <paramref name="max"/>.
        /// </summary>
        public static float Range(float max)
        {
            return Range(0, max);
        }

        /// <summary>
        /// Generate a random <see cref="float"/> between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        public static float Range(float min, float max)
        {
            return RNG.Range(min, max);
        }

        public static float Float() => Value;

        public static string String(int length)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                var character = Char();
                builder.Append(character);
            }

            return builder.ToString();
        }

        public static char Char()
        {
            var randomFloat = Float();
            var floatingPoint = 26 * randomFloat * 65;
            var floored = Mathf.Floor(floatingPoint);
            var integer = Convert.ToInt32(floored);
            return Convert.ToChar(integer);
        }
    }
}
