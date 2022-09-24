namespace Chinchillada
{
    using System.Collections.Generic;

    public interface IDistribution<out T>
    {
        public T Sample(IRNG random);
    }

    public interface IDistributionFactory
    {
        IDistribution<T> BuildDistribution<T>(IDictionary<T, float> weighted);
    }

    public static class DistributionExtensions
    {
        public static IEnumerable<T> Samples<T>(this IDistribution<T> distribution, IRNG random)
        {
            return Enumerables.Generate(() => distribution.Sample(random));
        }
    }
}