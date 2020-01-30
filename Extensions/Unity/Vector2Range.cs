using UnityEngine;

namespace Chinchillada.Utilities
{
    public static class Vector2Range
    {
        /// <summary>
        /// Checks if the <paramref name="value"/> is between the <see cref="range"/>.x and the <see cref="range"/>.y.
        /// </summary>
        /// <param name="range">
        /// <see cref="Vector2"/> that is interpreted as a range where the x is the inclusive lower bound and the y is the inclusive upper bounds.
        /// </param>
        /// <param name="value">The value we want io verify is within the <paramref name="range"/>.</param>
        /// <returns>True if the <paramref name="value"/> is within the <paramref name="range"/>, false if not.</returns>
        public static bool RangeContains(this Vector2 range, float value)
        {
            float minimum = range.x;
            float maximum = range.y;

            return value >= minimum && value <= maximum;
        }

        /// <summary>
        /// Generates a random value between the <paramref name="range"/>.x as the lower bound and the <paramref name="range"/>.y as the upper bound.
        /// </summary>
        public static float RandomInRange(this Vector2 range)
        {
            return Random.Range(range.x, range.y);
        }

        /// <summary>
        /// <see cref="Mathf.Lerp"/> the <paramref name="value"/> between the x and y of the <paramref name="range"/>.
        /// </summary>
        public static float RangeLerp(this Vector2 range, float value)
        {
            if (value <= 0)
                return range.x;

            if (value >= 1)
                return range.y;

            return Mathf.Lerp(range.x, range.y, value);
        }

        public static int RangeLerp(this Vector2Int range, float value)
        {
            return (int) Mathf.Lerp(range.x, range.y, value);
        }

        public static float RangeClamp(this Vector2 range, float value)
        {
            if (value < range.x)
                return range.x;

            if (value > range.y)
                return range.y;

            return value;
        }
    }
}