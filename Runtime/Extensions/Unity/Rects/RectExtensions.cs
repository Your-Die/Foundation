namespace Chinchillada
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using UnityEngine;

    public static class RectExtensions
    {
        public static (Rect bottomRect, Rect topRect) SplitHorizontal(this Rect rect, float splitPoint)
        {
            if (splitPoint < rect.yMin || splitPoint > rect.yMax)
                throw new InvalidOperationException();

            var bottomRect = new Rect(rect.xMin, rect.yMin, rect.width, splitPoint - rect.yMin);
            var topRect    = new Rect(rect.xMin, splitPoint, rect.width, rect.yMax - splitPoint);

            return (bottomRect, topRect);
        }

        public static Rect GrowPercentage(this Rect rect, float percentage)
        {
            var xExtra = (rect.width  * percentage) / 2f;
            var yExtra = (rect.height * percentage) / 2f;

            rect.xMin -= xExtra;
            rect.yMin -= yExtra;
            rect.xMax += xExtra;
            rect.yMax += yExtra;

            return rect;
        }

        public static (Rect leftRect, Rect rightRect) SplitVertical(this Rect rect, float splitPoint)
        {
            if (splitPoint < rect.xMin || splitPoint > rect.xMax)
                throw new InvalidOperationException();

            var leftRect  = new Rect(rect.xMin, rect.yMin, splitPoint - rect.xMin, rect.height);
            var rightRect = new Rect(splitPoint, rect.yMin, rect.xMax - splitPoint, rect.height);

            return (leftRect, rightRect);
        }

        public static float DistanceTo(this Rect rect, Rect other)
        {
            var boundingRect   = rect.BoundingBoxWith(other);
            var projectingRect = rect.ProjectTo(other);

            // Combined width/height is smaller if they overlap.
            var overlapWidth  = boundingRect.width  < rect.width  + other.width;
            var overlapHeight = boundingRect.height < rect.height + other.height;
            
            // If both overlap, the rects are intersecting.
            if (overlapWidth && overlapHeight)
                return 0f;

            if (overlapWidth)
                return Mathf.Abs(projectingRect.height);

            if (overlapHeight)
                return Mathf.Abs(projectingRect.width);

            return Vector2.Distance(projectingRect.min, projectingRect.max);
        }

        public static Rect BoundingBoxWith(this Rect rect, Rect other)
        {
            var xMin = Mathf.Min(rect.xMin, other.xMin);
            var yMin = Mathf.Min(rect.yMin, other.yMin);
            var xMax = Mathf.Max(rect.xMax, other.xMax);
            var yMax = Mathf.Max(rect.yMax, other.yMax);

            var width  = xMax - xMin;
            var height = yMax - xMin;

            return new Rect(xMin, yMin, width, height);
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

        public static Rect GetBoundingRect(this IEnumerable<Vector2> points)
        {
            var xMin = float.MaxValue;
            var yMin = float.MaxValue;
            var xMax = float.MinValue;
            var yMax = float.MinValue; 

            foreach (var point in points)
            {
                if (point.x < xMin) xMin = point.x;
                if (point.y < yMin) yMin = point.y;

                if (point.x > xMax) xMax = point.x;
                if (point.y > yMax) yMax = point.y;
            }

            return new Rect
            {
                xMin = xMin,
                xMax = xMax,
                yMin = yMin,
                yMax = yMax
            };
        }
    }
}