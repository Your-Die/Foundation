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

        public static bool TryGetComponent<T>(this Component context, out T component)
        {
            component = context.GetComponent<T>();
            return component != null;
        }

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

        public static float DistanceTo(this Transform transform, Transform other) => transform.DistanceTo(other.position);

        private static float DistanceTo(this Transform transform, Vector3 other) => transform.position.DistanceTo(other);

        public static float DistanceTo(this Vector3 position, Vector3 other) => Vector3.Distance(position, other);
    }
}
