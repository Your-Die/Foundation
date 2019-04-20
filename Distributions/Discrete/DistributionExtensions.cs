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
        public static IDiscreteDistribution<T> Where<T>(this IDiscreteDistribution<T> distribution,
            Func<T, bool> predicate)
        {
            var validSupport = distribution.Support().Where(predicate).EnsureList();
            var weights = validSupport.Select(distribution.Weight);

            return validSupport.ToWeighted(weights);
        }

        public static IDiscreteDistribution<T> ToUniform<T>(this IEnumerable<T> items)
        {
            var list = items.EnsureList();

            var indices = list.IndexDistribution();
            return indices.Select(i => list[i]);
        }

        public static IDiscreteDistribution<T> ToWeighted<T>(this IEnumerable<T> items, IEnumerable<int> weights)
        {
            var list = items.EnsureList();
            var weightDistribution = WeightedInteger.Distribution(weights.ToArray());
            return weightDistribution.Select(i => list[i]);
        }

        public static IDiscreteDistribution<T> ToWeighted<T>(this IEnumerable<T> items, params int[] weights)
        {
            items.ToWeighted(weights.AsEnumerable());
        }

        public static IDiscreteDistribution<int> IndexDistribution<T>(this IList<T> list)
        {
            return SDU.Distribution(0, list.Count - 1);
        }
    }
}
