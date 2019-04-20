namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;
    using Utilities;
    using SDU = StandardDiscreteUniform;

    public static class DistributionExtensions
    {
        public static IDiscreteDistribution<R> Select<A, R>(
            this IDiscreteDistribution<A> distribution,
            Func<A, R> projection)
        {
            return Projected<A, R>.Distribution(distribution, projection);
        }

        public static IDiscreteDistribution<T> ToUniform<T>(this IEnumerable<T> items)
        {
            var list = items.EnsureList();

            var indices = list.IndexDistribution();
            return indices.Select(i => list[i]);
        }

        public static IDiscreteDistribution<int> IndexDistribution<T>(this IList<T> list)
        {
            return SDU.Distribution(0, list.Count - 1);
        }
    }
}
