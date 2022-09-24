namespace Chinchillada.Distributions
{
    using System.Collections.Generic;

    public interface IDiscreteDistribution<T> : IDistribution<T>
    {
        IEnumerable<T> Support();
        int Weight(T item);
    }
}