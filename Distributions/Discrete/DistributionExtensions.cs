using System.Linq;

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
            this IDiscreteDistribution<TPrior> prior,
            Func<TPrior, IDiscreteDistribution<TSample>> likelihood,
            Func<TPrior, TSample, TProjection> projection)
        {
            var priorSupport = prior.Support().ToList();
            var priorWeights = priorSupport.Select(a => likelihood(a).TotalWeight());
            int lcm = priorWeights.LCM();
            
            var weights =
                from a in priorSupport                   // Iterate over support
                let weight = prior.Weight(a)                // get weight.
                let probability = likelihood(a)             // Get inner distribution
                let totalWeight = probability.TotalWeight() // Sum weight of inner distribution
                from support in probability.Support()       // iterate inner distribution support
                group weight * probability.Weight(support)  // "Combine Fractions" by multiplication
                             * lcm / totalWeight
                    by projection(a, support);

            var dictionary = weights.ToDictionary(group => group.Key, group => group.Sum());
            return dictionary.Keys.ToWeighted(dictionary.Values);
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
