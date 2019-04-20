using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Distributions
{
    public class Combined<A, B, C> : IDiscreteDistribution<C>
    {
        private readonly List<C> _support;

        private readonly IDiscreteDistribution<A> _prior;

        private readonly Func<A, IDiscreteDistribution<B>> _likelihood;

        private readonly Func<A, B, C> _projection;

        public static IDiscreteDistribution<C> Distribution(
            IDiscreteDistribution<A> prior,
            Func<A, IDiscreteDistribution<B>> likelihood,
            Func<A, B, C> projection)
        {
            return new Combined<A, B, C>(prior, likelihood, projection);
        }

        private Combined(IDiscreteDistribution<A> prior,
            Func<A, IDiscreteDistribution<B>> likelihood,
            Func<A, B, C> projection)
        {
            _prior = prior;
            _likelihood = likelihood;
            _projection = projection;

            var support =
                from a in prior.Support()
                from b in likelihood(a).Support()
                select _projection(a, b);

            _support = support.Distinct().ToList();
        }

        public C Sample()
        {
            var priorSample = _prior.Sample();
            var distribution = _likelihood(priorSample);
            var sample = distribution.Sample();

            return _projection(priorSample, sample);
        }

        public IEnumerable<C> Support() => _support.AsEnumerable();

        public int Weight(C variable) => throw new System.NotImplementedException();
    }
}