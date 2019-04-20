using System.Linq;
using NUnit.Framework.Internal;

namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;
    using Utilities;
    using SDU = StandardDiscreteUniform;

    public static class DistributionExtensions
    {
        public static IDiscreteDistribution<TResult> Select<TSource, TResult>(
            this IDiscreteDistribution<TSource> distribution,
            Func<TSource, TResult> projection)
        {
            var dictionary = distribution.Support()
                .GroupBy(projection, distribution.Weight)
                .ToDictionary(group => group.Key, group => group.Sum());

            var keys = dictionary.Keys.EnsureList();
            var values = keys.Select(key => dictionary[key]);

            var weighted = WeightedInteger.Distribution(values.ToArray());
            return Projected<int, TResult>.Distribution(weighted, i => keys[i]);
        }

        public static IDiscreteDistribution<T> Where<T>(this IDiscreteDistribution<T> distribution,
            Func<T, bool> predicate)
        {
            var validSupport = distribution.Support().Where(predicate).EnsureList();
            var weights = validSupport.Select(distribution.Weight);

            return validSupport.ToWeighted(weights);
        }

        public static IDiscreteDistribution<TProjection> SelectMany<TPrior, TSample, TProjection>(
            this IDiscreteDistribution<TPrior> distribution,
            Func<TPrior, IDiscreteDistribution<TSample>> likelihood,
            Func<TPrior, TSample, TProjection> projection)
        {
            return Combined<TPrior, TSample, TProjection>.Distribution(distribution, likelihood, projection);
        }

        public static IDiscreteDistribution<TResult> SelectMany<TPrior, TResult>(
            this IDiscreteDistribution<TPrior> distribution,
            Func<TPrior, IDiscreteDistribution<TResult>> likelihood)
        {
            return distribution.SelectMany(likelihood, (a, b) => b);
        }

        public static IDiscreteDistribution<(TPrior, TResult)> Joint<TPrior, TResult>(
            this IDiscreteDistribution<TPrior> distribution,
            Func<TPrior, IDiscreteDistribution<TResult>> likelihood)
        {
            return distribution.SelectMany(likelihood, (a, b) => (a,b));
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
            return items.ToWeighted(weights.AsEnumerable());
        }

        public static IDiscreteDistribution<int> IndexDistribution<T>(this IList<T> list)
        {
            return SDU.Distribution(0, list.Count - 1);
        }

        public static int TotalWeight<T>(this IDiscreteDistribution<T> distribution)
        {
            var support = distribution.Support();
            var weights = support.Select(distribution.Weight);

            return weights.Sum();
        }
    }
}
