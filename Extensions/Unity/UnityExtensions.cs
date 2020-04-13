﻿using System.Collections.Generic;
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

        public static float DistanceTo(this Transform transform, Transform other) => transform.DistanceTo(other.position);

        private static float DistanceTo(this Transform transform, Vector3 other) => transform.position.DistanceTo(other);

        public static float DistanceTo(this Vector3 position, Vector3 other) => Vector3.Distance(position, other);

        public static (int x, int y, int z) ToTuple(this Vector3Int vector) => (vector.x, vector.y, vector.z);
        public static Vector3 ToVector3(this Vector2Int vector)
        {
            return new Vector3(vector.x, vector.y);
        }

        public static Vector2Int ToVector2(this Vector3Int vector) => new Vector2Int(vector.x, vector.y);

        public static Vector3Int GetCenterInt(this BoundsInt bounds)
        {
            return bounds.position + bounds.size / 2;
        }

        public static bool Contains2D(this BoundsInt bounds, Vector2Int vector)
        {
            return vector.x >= bounds.xMin && vector.x < bounds.xMax &&
                   vector.y >= bounds.yMin && vector.y < bounds.yMax;
        }

        public static IEnumerable<int> AsRange(this Vector2Int vector)
        {
            for (var value = vector.x; value <= vector.y; value++)
                yield return value;
        }
    }
}
