using System.Collections.Generic;

namespace Chinchillada.Distributions
{
    public interface IDiscreteDistribution<T> : IDistribution<T>
    {
        IEnumerable<T> Support();

        int Weight(T variable);
    }
}
