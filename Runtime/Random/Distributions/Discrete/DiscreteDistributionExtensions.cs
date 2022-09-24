namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;
    using SDU = StandardDiscreteUniform;

    public static class DiscreteDistributionExtensions
    {
        public static IDiscreteDistribution<T> ToUniform<T>(this IReadOnlyList<T> items)
        {
            return from index in SDU.Distribution(0, items.Count - 1)
                   select items[index];
        }

        public static IDiscreteDistribution<TOutput> Select<TInput, TOutput>(
            this IDiscreteDistribution<TInput> distribution,
            ITransformer<TInput, TOutput>      projection)
        {
            return Projected<TInput, TOutput>.Distribution(distribution, projection);
        }

        public static IDiscreteDistribution<TOutput> Select<TInput, TOutput>(
            this IDiscreteDistribution<TInput> distribution,
            Func<TInput, TOutput>              projection)
        {
            var transformer = new FuncTransformer<TInput, TOutput>(projection);
            return Select(distribution, transformer);
        }
    }
}