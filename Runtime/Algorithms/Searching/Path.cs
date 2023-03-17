using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Algorithms
{
    using System;

    public static class Path
    {
        public static IEnumerable<T> Build<T>(IReadOnlyList<T> startStates, T end, IReadOnlyDictionary<T, T> actions)
        {
            var current  = end;

            while (!startStates.Contains(current))
            {
                yield return current;
                current = actions[current];
            }

            yield return current;
        }
    }
}