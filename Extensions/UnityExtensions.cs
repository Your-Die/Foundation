using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada.Utilities
{
    public static class UnityExtensions
    {
        public static bool RangeContains(this Vector2 range, float value)
        {
            float minimum = range.x;
            float maximum = range.y;

            return value >= minimum && value <= maximum;
        }

        public static IEnumerable<T> GetComponentsInDirectChildren<T>(this Component component)
        {
            Transform transform = component.transform;
            return transform.GetComponentsInDirectChildren<T>();
        }

        public static IEnumerable<T> GetComponentsInDirectChildren<T>(this GameObject gameObject)
        {
            Transform transform = gameObject.transform;
            return transform.GetComponentsInDirectChildren<T>();
        }

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
