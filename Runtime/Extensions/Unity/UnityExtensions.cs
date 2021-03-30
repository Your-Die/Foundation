using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// Static class containing extension methods for Unity types.
    /// </summary>
    public static class UnityExtensions
    {
        public static void SetScaleX(this Transform transform, float scaleX)
        {
            var scale = transform.localScale;
            scale.x = scaleX;
            transform.localScale = scale;
        }
        
        public static float DistanceTo(this Transform transform, Transform other) => transform.DistanceTo(other.position);

        private static float DistanceTo(this Transform transform, Vector3 other) => transform.position.DistanceTo(other);

        public static float DistanceTo(this Vector3 position, Vector3 other) => Vector3.Distance(position, other);
        

        public static void ClearAndDestroy<T>(this IList<T> list) where T : Component
        {
            foreach (var item in list) 
                Object.Destroy(item.gameObject);
            
            list.Clear();
        }

        public static IEnumerable<Transform> GetChildren(this Transform transform)
        {
            for (var index = 0; index < transform.childCount; index++)
                yield return transform.GetChild(index);
        }
    }
}
