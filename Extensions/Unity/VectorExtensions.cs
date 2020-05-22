using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada.Foundation
{
    public static class VectorExtensions
    {
        public static Vector3 Average(this IEnumerable<Vector3> vectors)
        {
            var list = vectors.ToList();
            var sum = list.Sum();

            return sum / list.Count;
        }

        public static Vector3 Sum(this IEnumerable<Vector3> vectors)
        {
            return vectors.Aggregate(Vector3.zero, (current, vector) => current + vector);
        }
    }
}