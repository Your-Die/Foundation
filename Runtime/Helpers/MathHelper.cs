﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada
{
    using UnityEngine;

    /// <summary>
    /// Static class of math functions.
    /// </summary>
    public static class MathHelper
    {
        public static float Power(float value, int power)
        {
            var result = 1f;

            for (var i = 1; i <= power; i++) 
                result *= value;

            return result;
        }
        
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
            float minAsFloat = min;
            float maxAsFloat = max;

            //Call float overload.
            float percentage = PercentageBetween(pointAsFloat, minAsFloat, maxAsFloat) * 100;

            //Cast back to int.
            return (int) percentage;
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
            if(rangeSize < 0)
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
            var gcd = GCD(x, y);
            var product = x * y;

            return product / gcd;
        }

        public static IEnumerable<int> ShrinkValues(IEnumerable<int> values)
        {
            var valueList = values.EnsureList();
            int greatestCommonDivider = valueList.GCD();

            return valueList.Select(value => value / greatestCommonDivider);
        }

        public static int ClosestSmallerMultiple(this int value, int multiple)
        {
            var division = value / multiple; // Floors implicitly.
            return division * multiple;
        }
        
        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }
        public static Vector2 RadianToVector2(float radian, float length)
        {
            return RadianToVector2(radian) * length;
        }
        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }
        public static Vector2 DegreeToVector2(float degree, float length)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad) * length;
        }
    }
}
