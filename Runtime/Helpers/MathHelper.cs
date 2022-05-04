namespace Chinchillada.Foundation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public struct Circle
    {
        public Vector2 Position { get; set; }
        
        public float Radius { get; set; }

        public bool IntersectsWith(Line line)
        {
            var delta    = line.GetDelta();
            var distance = line.Start - this.Position;

            var a = Vector2.Dot(delta, delta);
            var b = 2 * Vector2.Dot(distance, delta);
            var c = Vector2.Dot(distance, distance) - this.Radius * this.Radius;

            var discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
                return false;

            // ray didn't totally miss sphere,
            // so there is a solution to
            // the equation.

            discriminant = Mathf.Sqrt(discriminant);

            // either solution may be on or off the ray so need to test both
            // t1 is always the smaller value, because BOTH discriminant and
            // a are nonnegative.
            var t1 = (-b - discriminant) / (2 * a);
            var t2 = (-b + discriminant) / (2 * a);

            // 3x HIT cases:
            //          -o->             --|-->  |            |  --|->
            // Impale(t1 hit,t2 hit), Poke(t1 hit,t2>1), ExitWound(t1<0, t2 hit), 

            // 3x MISS cases:
            //       ->  o                     o ->              | -> |
            // FallShort (t1>1,t2>1), Past (t1<0,t2<0), CompletelyInside(t1<0, t2>1)


            // t1 is the intersection, and it's closer than t2
            // (since t1 uses -b - discriminant)
            // Impale, Poke
            if (t1 >= 0 && t1 <= 1)
                return true;

            // here t1 didn't intersect so we are either started
            // inside the sphere or completely past it
            // ExitWound
            if (t2 >= 0 && t2 <= 1)
                return true;

            // CompletelyInside
            if (t1 < 0 && t2 > 1)
                return true;

            // no intn: FallShort, Past, CompletelyInside
            return false;
        }

        public void DrawGizmo(float height)
        {
            var position3D = new Vector3(this.Position.x, height, this.Position.y);
            
            Gizmos.DrawWireSphere(position3D, this.Radius);
        }
    }
    
    public struct Line
    {
        public Vector2 Start { get; set; }
        public Vector2 End   { get; set; }

        public void Shrink(float extremitySize)
        {
            var delta     = (this.End - this.Start).normalized;
            var extremity = delta * extremitySize;

            this.Start += extremity;
            this.End   -= extremity;
        }

        public Vector2 GetDelta() => this.End - this.Start;

        public bool IntersectsWith(Line other)
        {
            var deltaA = this.End  - this.Start;
            var deltaB = other.End - other.Start;

            var s = (-deltaA.y * (this.Start.x - other.Start.x) +
                     deltaA.x  * (this.Start.y - other.Start.y)) /
                    (-deltaB.x * deltaA.y + deltaA.x * deltaB.y);
            var t = (deltaB.x * (this.Start.y - other.Start.y) -
                     deltaB.y * (this.Start.x - other.Start.x)) /
                    (-deltaB.x * deltaA.y + deltaA.x * deltaB.y);

            return s >= 0 && s <= 1 && t >= 0 && t <= 1;
        }

        public void DrawGizmo(float height)
        {
            var start3D = To3D(this.Start);
            var end3D   = To3D(this.End);

            Gizmos.DrawLine(start3D, end3D);
            
            Vector3 To3D(Vector2 point) => new Vector3(point.x, height, point.y);
        }
    }

    /// <summary>
    /// Static class of math functions.
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// Returns the percentage of the point between the min and max.
        /// </summary>
        /// <param name="point">The point we want the percentage of.</param>
        /// <param name="min">The minimum value of the range.</param>
        /// <param name="max">The maximum value of the range.</param>
        /// <returns></returns>
        public static float PercentageBetween(float point, float min, float max)
        {
            return (point - min) / (max - min);
        }

        /// <summary>
        /// Returns the percentage of the point between the min and max.
        /// </summary>
        /// <param name="point">The point we want the percentage of.</param>
        /// <param name="min">The minimum value of the range.</param>
        /// <param name="max">The maximum value of the range.</param>
        /// <returns></returns>
        public static int PercentageBetween(int point, int min, int max)
        {
            //Cast to float.
            float pointAsFloat = point;
            float minAsFloat   = min;
            float maxAsFloat   = max;

            //Call float overload.
            float percentage = PercentageBetween(pointAsFloat, minAsFloat, maxAsFloat) * 100;

            //Cast back to int.
            return (int)percentage;
        }

        /// <summary>
        /// Returns a range of integers from <paramref name="min"/> to <paramref name="max"/>.
        /// </summary>
        /// <param name="min">The first value in the sequence.</param>
        /// <param name="max">The upper bound.</param>
        /// <param name="stepSize">The difference between subsequent values.</param>
        /// <returns></returns>
        public static IEnumerable<int> GetRange(int min, int max, int stepSize = 1)
        {
            int rangeSize = max - min;
            if (rangeSize < 0)
                throw new ArgumentException();

            for (int value = min; value < max; value += stepSize)
                yield return value;
        }

        public static (int smaller, int bigger) SortPair(int x, int y)
        {
            return x >= y ? (x, y) : (y, x);
        }

        /// <summary>
        /// Uses the <paramref name="stepSize"/> to step from <paramref name="min"/> until <paramref name="max"/> is reached or exceeded.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="stepSize"></param>
        /// <returns></returns>
        public static IEnumerable<float> GetRange(float min, float max, float stepSize)
        {
            var rangeSize = max - min;
            if (rangeSize < 0)
                throw new ArgumentException();

            for (var value = min; value < max; value += stepSize)
                yield return value;
        }

        /// <summary>
        /// Calculates the greatest common divider between <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        public static int GCD(int x, int y)
        {
            while (true)
            {
                if (y == 0)
                    return x;

                var copyX = x;
                x = y;
                y = copyX % y;
            }
        }

        /// <summary>
        /// Calculates the lowest common multiple between <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        public static int LCM(int x, int y)
        {
            var gcd     = GCD(x, y);
            var product = x * y;

            return product / gcd;
        }

        public static IEnumerable<int> ShrinkValues(IEnumerable<int> values)
        {
            var valueList             = values.EnsureList();
            int greatestCommonDivider = valueList.GCD();

            return valueList.Select(value => value / greatestCommonDivider);
        }

        public static int ClosestSmallerMultiple(this int value, int multiple)
        {
            var division = value / multiple; // Floors implicitly.
            return division * multiple;
        }

        public static bool CircleLineIntersection(Vector2 circleCenter, float   circleRadius,
                                                  Vector2 lineStart,    Vector2 lineEnd)
        {
            var delta    = lineEnd   - lineStart;
            var distance = lineStart - circleCenter;

            var a = Vector2.Dot(delta, delta);
            var b = 2 * Vector2.Dot(distance, delta);
            var c = Vector2.Dot(distance, distance) - circleRadius * circleRadius;

            var discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
                return false;

            // ray didn't totally miss sphere,
            // so there is a solution to
            // the equation.

            discriminant = Mathf.Sqrt(discriminant);

            // either solution may be on or off the ray so need to test both
            // t1 is always the smaller value, because BOTH discriminant and
            // a are nonnegative.
            var t1 = (-b - discriminant) / (2 * a);
            var t2 = (-b + discriminant) / (2 * a);

            // 3x HIT cases:
            //          -o->             --|-->  |            |  --|->
            // Impale(t1 hit,t2 hit), Poke(t1 hit,t2>1), ExitWound(t1<0, t2 hit), 

            // 3x MISS cases:
            //       ->  o                     o ->              | -> |
            // FallShort (t1>1,t2>1), Past (t1<0,t2<0), CompletelyInside(t1<0, t2>1)


            // t1 is the intersection, and it's closer than t2
            // (since t1 uses -b - discriminant)
            // Impale, Poke
            if (t1 >= 0 && t1 <= 1)
                return true;

            // here t1 didn't intersect so we are either started
            // inside the sphere or completely past it
            // ExitWound
            if (t2 >= 0 && t2 <= 1)
                return true;

            // CompletelyInside
            if (t1 < 0 && t2 > 1)
                return true;

            // no intn: FallShort, Past, CompletelyInside
            return false;
        }

        public static bool Approximately(Vector3 a, Vector3 b)
        {
            return Mathf.Approximately(a.x, b.x) &&
                   Mathf.Approximately(a.y, b.y) &&
                   Mathf.Approximately(a.z, b.z);
        }
    }
}