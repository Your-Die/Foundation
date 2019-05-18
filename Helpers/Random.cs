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
    }
}
