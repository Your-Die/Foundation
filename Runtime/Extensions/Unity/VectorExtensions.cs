using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada
{
    using System;

    public static class VectorExtensions
    {
        public static (int x, int y, int z) ToTuple(this Vector3Int vector) => (vector.x, vector.y, vector.z);
        public static Vector3 ToVector3(this Vector2Int vector) => new Vector3(vector.x, vector.y);
        public static Vector3Int ToVector3Int(this Vector2Int vector) => new Vector3Int(vector.x, vector.y, 0);

        public static Vector2Int ToVector2(this Vector3Int vector) => new Vector2Int(vector.x, vector.y);
        
        public static IEnumerable<int> AsRange(this Vector2Int vector)
        {
            for (var value = vector.x; value <= vector.y; value++)
                yield return value;
        }

        public static Vector2Int XY(this Vector3Int vector) => new Vector2Int(vector.x, vector.y);

        public static Vector2 XY(this Vector3 vector) => new Vector2(vector.x, vector.y);

        public static Vector2 XZ(this Vector3 vector3) => new Vector2(vector3.x, vector3.z);
        
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

        public static Vector2Int DivideElementWise(this Vector2Int vector, Vector2Int divider)
        {
            return new Vector2Int
            {
                x = vector.x / divider.x,
                y = vector.y / divider.y
            };
        }

        public static Vector2Int ToInt(this Vector2 vector)
        {
            return new Vector2Int
            {
                x = (int)vector.x,
                y = (int)vector.y,
            };
        }

        public static Bounds GetBounds(this IEnumerable<Vector3> points)
        {
            var minX = float.MaxValue;
            var minY = float.MaxValue;
            var minZ = float.MaxValue;
            var maxX = float.MinValue;
            var maxY = float.MinValue;
            var maxZ = float.MinValue;

            foreach (var point in points)
            {
                if (point.x < minX) minX = point.x;
                if (point.y < minY) minY = point.y;
                if (point.z < minZ) minZ = point.z;

                if (point.x > maxX) maxX = point.x;
                if (point.y > maxY) maxY = point.y;
                if (point.z > maxZ) maxZ = point.z;
            }
            
            var min = new Vector3(minX, minY, minZ);
            var max = new Vector3(maxX, maxY, maxZ);

            var size = max - min;
            var center = (max + min) / 2f;
            
            return new Bounds(center, size);
        }

        public static Bounds GetBounds(this IEnumerable<Vector2> points)
        {
            var minX = float.MaxValue;
            var minY = float.MaxValue;
            var maxX = float.MinValue;
            var maxY = float.MinValue;

            foreach (var point in points)
            {
                if (point.x < minX) minX = point.x;
                if (point.y < minY) minY = point.y;

                if (point.x > maxX) maxX = point.x;
                if (point.y > maxY) maxY = point.y;
            }
            
            var min = new Vector2(minX, minY);
            var max = new Vector2(maxX, maxY);

            var size = max - min;
            var center = (max + min) / 2f;
            
            return new Bounds(center, size);
        }

        /// <returns>
        /// the orthogonal neighbors of the <paramref name="vector"/>,
        /// in clockwise order starting at up.
        /// </returns>
        public static IEnumerable<Vector2Int> GetNeighbors(this Vector2Int vector)
        {
            yield return vector + Vector2Int.up;
            yield return vector + Vector2Int.right;
            yield return vector + Vector2Int.down;
            yield return vector + Vector2Int.left;
        }

        public static int ManhattanDistance(this Vector2Int vector, Vector2Int other)
        {
            var xDistance = Mathf.Abs(vector.x - other.x);
            var yDistance = Mathf.Abs(vector.y - other.y);

            return xDistance + yDistance;
        }

        public static float DistanceToXZ(this Vector3 from, Vector3 to)
        {
            var fromXZ = from.XZ();
            var toXZ   = to.XZ();

            return Vector2.Distance(fromXZ, toXZ);
        }
    }
}