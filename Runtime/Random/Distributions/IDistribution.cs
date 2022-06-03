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
}