using System.Collections.Generic;

namespace Chinchillada.Algorithms
{
    using System;

    public static class Path
    {
        public static IEnumerable<T> Build<T>(T start, T end, IReadOnlyDictionary<T, T> actions)
        {
            var current  = end;

            while (!Equals(current, start))
            {
                yield return current;
                current = actions[current];
            }

            yield return start;
        }
    }
}