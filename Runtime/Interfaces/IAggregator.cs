namespace Chinchillada
{
    using System.Collections.Generic;
    using System.Linq;

    public interface IAggregator<T>
    {
        public T Aggregate(IEnumerable<T> values);
    }
    
    public class Sum : IAggregator<float>
    {
        public float Aggregate(IEnumerable<float> values) => values.Sum();
    }

    public class Mean : IAggregator<float>
    {
        public float Aggregate(IEnumerable<float> values) => values.Average();
    }
    
    public class Min : IAggregator<float>
    {
        public float Aggregate(IEnumerable<float> values) => values.Min();
    }
    
    public class Max : IAggregator<float>
    {
        public float Aggregate(IEnumerable<float> values) => values.Max();
    }
}