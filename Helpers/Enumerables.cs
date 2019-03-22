using System.Collections.Generic;

namespace Chinchillada.Utilities
{
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
    }
}