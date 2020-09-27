using System;
using System.Collections;
using UnityEngine;

namespace Chinchillada.Foundation
{
    public static class MiscExtensions
    {
        public static Vector3 ToVector(this Color color)
        {
            return new Vector3(color.r, color.g, color.b);
        }
        
        public static int ToBinary(this bool value)
        {
            return value ? 1 : 0;
        }

        public static BitArray ToBitArray(this byte number)
        {
            return new BitArray(new [] {number});
        }

        public static T Until<T>(this Func<T> generator, Func<T, bool> predicate)
        {
            T generation;
            do
            {
                generation = generator();
            } while (!predicate(generation));

            return generation;
        }

        public static bool IsUneven(this int value) => value.IsEven() == false;

        public static bool IsEven(this int value) => value % 2 == 0;

        public static (int width, int height) GetShape<T>(this T[,] array)
        {
            var width = array.GetLength(0);
            var height = array.GetLength(1);

            return (width, height);
        }
    }
}
