using System;
using System.Collections.Generic;
using System.Linq;
using Chinchillada.Utilities;

namespace Chinchillada.Distributions
{
    public class Projected<A, R> : IDiscreteDistribution<R>
    {
        private readonly IDiscreteDistribution<A> _underlying;

        private readonly Func<A, R> _projection;

        private readonly Dictionary<R, int> _weights;

        public static IDiscreteDistribution<R> Distribution(IDiscreteDistribution<A> underlying, Func<A, R> projection)
        {
            var result = new Projected<A, R>(underlying, projection);
            var support = result.Support().ToList();

            if (support.Count != 1)
                return result;

            R item = support.First();
            return Singleton<R>.Distribution(item);
        }

        private Projected(IDiscreteDistribution<A> underlying, Func<A, R> projection)
        {
            _underlying = underlying;
            _projection = projection;

            _weights = underlying.Support()
                .GroupBy(projection, underlying.Weight)
                .ToDictionary(group => group.Key, group => group.Sum());
        }

        public R Sample()
        {
            A underlyingSample = _underlying.Sample();
            return _projection(underlyingSample);
        }

        public IEnumerable<R> Support() => _weights.Keys;

        public int Weight(R variable) => _weights.GetValueOrDefault(variable);
    }
}
