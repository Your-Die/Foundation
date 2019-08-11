using System;
using System.Runtime.CompilerServices;

namespace Chinchillada.Utilities
{
    public static class MiscExtensions
    {
        public static int ToBinary(this bool value)
        {
            return value ? 1 : 0;
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

        public static int AsBinary(this bool value) => value ? 1 : 0;
    }
}
