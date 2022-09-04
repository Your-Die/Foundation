namespace Chinchillada
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public static class RectExtensions
    {
        public static IEnumerable<Vector2Int> EnumerateCells(this RectInt rect)
        {
            for (var x = rect.xMin; x <= rect.xMax; x++)
            for (var y = rect.yMin; y <= rect.yMax; y++)
                yield return new Vector2Int(x, y);
        }

        public static (Rect bottomRect, Rect topRect) SplitHorizontal(this Rect rect, float splitPoint)
        {
            if (splitPoint < rect.yMin || splitPoint > rect.yMax)
                throw new InvalidOperationException();

            var bottomRect = new Rect(rect.xMin, rect.yMin, rect.width, splitPoint - rect.yMin);
            var topRect    = new Rect(rect.xMin, splitPoint, rect.width, rect.yMax - splitPoint);

            return (bottomRect, topRect);
        }

        public static (Rect leftRect, Rect rightRect) SplitVertical(this Rect rect, float splitPoint)
        {
            if (splitPoint < rect.xMin || splitPoint > rect.xMax)
                throw new InvalidOperationException();

            var leftRect  = new Rect(rect.xMin, rect.yMin, splitPoint - rect.xMin, rect.height);
            var rightRect = new Rect(splitPoint, rect.yMin, rect.xMax - splitPoint, rect.height);

            return (leftRect, rightRect);
        }

        public static Rect ProjectTo(this Rect from, Rect to)
        {
            var xMin = Mathf.Max(from.xMin, to.xMin);
            var yMin = Mathf.Max(from.yMin, to.yMin);
            var xMax = Mathf.Min(from.xMax, to.xMax);
            var yMax = Mathf.Min(from.yMax, to.yMax);

            var width  = xMax - xMin;
            var height = yMax - yMin;

            return new Rect(xMin, yMin, width, height);
        }

        public static bool Contains(this Rect rect, Rect other)
        {
            return rect.xMin <= other.xMin
                && rect.yMin <= other.yMin
                && rect.xMax >= other.xMax
                && rect.yMax >= other.yMax;
        }

        public static bool Contains(this Rect rect, Vector2 vector)
        {
            return rect.xMin <= vector.x && vector.x  <= rect.xMax
                                         && rect.yMin <= vector.y && vector.y <= rect.yMax;
        }
    }
}