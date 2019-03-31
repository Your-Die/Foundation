using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Static class containing extension methods for Unity types.
    /// </summary>
    public static class UnityExtensions
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
        /// Generates a random point within the <paramref name="bounds"/>.
        /// </summary>
        public static Vector3 RandomPoint(this Bounds bounds)
        {
            Vector3 min = bounds.center - bounds.size;
            Vector3 max = bounds.center + bounds.size;

            return new Vector3
            {
                x = Random.Range(min.x, max.x),
                y = Random.Range(min.y, max.y),
                z = Random.Range(min.z, max.z)
            };
        }

        /// <summary>
        /// Tries to find a <see cref="Component"/> of the given type in the first layer of children of the <paramref name="component"/>.
        /// </summary>
        public static IEnumerable<T> GetComponentsInDirectChildren<T>(this Component component)
        {
            Transform transform = component.transform;
            return transform.GetComponentsInDirectChildren<T>();
        }

        /// <summary>
        /// Tries to find a <see cref="Component"/> of the given type in the first layer of children of the <paramref name="gameObject"/>.
        /// </summary>
        public static IEnumerable<T> GetComponentsInDirectChildren<T>(this GameObject gameObject)
        {
            Transform transform = gameObject.transform;
            return transform.GetComponentsInDirectChildren<T>();
        }

        /// <summary>
        /// Tries to find a <see cref="Component"/> of the given type in the first layer of children of the <paramref name="transform"/>.
        /// </summary>
        public static IEnumerable<T> GetComponentsInDirectChildren<T>(this Transform transform)
        {
            int childCount = transform.childCount;
            for (int index = 0; index < childCount; index++)
            {
                Transform childTransform = transform.GetChild(index);
                T component = childTransform.GetComponent<T>();

                if (component != null)
                    yield return component;
            }
        }
    }
}
