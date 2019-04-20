using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Distributions
{
    public class Combined<A, R> : IDiscreteDistribution<R>
    {
        private readonly List<R> _support;

        private readonly IDiscreteDistribution<A> _prior;

        private readonly Func<A, IDiscreteDistribution<R>> _likelihood;

        public static IDiscreteDistribution<R> Distribution(IDiscreteDistribution<A> prior,
            Func<A, IDiscreteDistribution<R>> likelihood) => new Combined<A, R>(prior, likelihood);

        private Combined(IDiscreteDistribution<A> prior,
            Func<A, IDiscreteDistribution<R>> likelihood)
        {
            _prior = prior;
            _likelihood = likelihood;

            var support = from priorSupport in prior.Support()
                let distribution = likelihood(priorSupport)
                from likelihoodSupport in distribution.Support()
                select likelihoodSupport;

            _support = support.Distinct().ToList();
        }

        public R Sample()
        {
            var priorSample = _prior.Sample();
            var distribution = _likelihood(priorSample);
            return distribution.Sample();
        }

        public IEnumerable<R> Support() => _support.AsEnumerable();

        public int Weight(R variable) => throw new System.NotImplementedException();
    }

