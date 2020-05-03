using System;
using System.Collections.Generic;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Helper class for simple enumerable constructors.
    /// </summary>
    public static class Enumerables
    {
        public static IEnumerable<T> Single<T>(T element)
        {
            yield return element;
        }

        public static IEnumerable<T> Repeat<T>(T element)
        {
            while (true)
                yield return element;
        }

        public static IEnumerable<T> Generate<T>(Func<T> factory)
        {
            while (true)
                yield return factory.Invoke();
        }
    }
}