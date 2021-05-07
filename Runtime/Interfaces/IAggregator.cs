namespace Chinchillada
{
    using System.Collections.Generic;
    using System.Linq;

    public interface IAggregator<T>
    {
        public T Aggregate(IEnumerable<T> values);
    }
    
    public class SumAggregator : IAggregator<float>
    {
        public float Aggregate(IEnumerable<float> values) => values.Sum();
    }

    public class AverageAggregator : IAggregator<float>
    {
        public float Aggregate(IEnumerable<float> values) => values.Average();
    }
}