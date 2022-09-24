namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Generation;
    using UnityEditor;

    public class Projected<TUnderlying, TResult> : IDiscreteDistribution<TResult>
    {
        private IDiscreteDistribution<TUnderlying> underlying;
        private ITransformer<TUnderlying, TResult> projection;

        private Dictionary<TResult, int> weights;

        private Projected(IDiscreteDistribution<TUnderlying> underlying, ITransformer<TUnderlying, TResult> projection)
        {
            this.underlying = underlying;
            this.projection = projection;

            this.EnsureWeights();
        }

        public static IDiscreteDistribution<TResult> Distribution(IDiscreteDistribution<TUnderlying> underlying,
                                                                  ITransformer<TUnderlying, TResult> projection)
        {
            var projected  = new Projected<TUnderlying, TResult>(underlying, projection);
            
            var support = projected.Support().ToArray();
            if (support.Length == 1)
            {
                var item = support.First();
                return Singleton<TResult>.Distribution(item);
            }

            return projected;
        }

        public TResult Sample(IRNG random)
        {
            var sample = this.underlying.Sample(random);
            return this.projection.Transform(sample);
        }

        public IEnumerable<TResult> Support()
        {
            this.EnsureWeights();
            return this.weights.Keys;
        }

        public int Weight(TResult item)
        {
            this.EnsureWeights();
            return this.weights.GetValueOrDefault(item);
        }

        private void EnsureWeights()
        {
            this.weights ??= this.underlying.Support()
                                 .GroupBy(this.projection.Transform, this.underlying.Weight)
                                 .ToDictionary(group => group.Key, 
                                               group => group.Sum());
        }
    }
}