using Chinchillada.Distributions;

namespace Chinchillada.Distributions
{
    public interface IWeightedDistribution<T> : IDistribution<T>
    {
        double Weight(T item);
    }
}
