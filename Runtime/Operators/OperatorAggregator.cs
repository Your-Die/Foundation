namespace Chinchillada
{
    using System.Collections.Generic;
    using System.Linq;
    using Sirenix.Serialization;

    public class OperatorAggregator<T> : IAggregator<T>
    {
        [OdinSerialize] private IOperator<T, T> @operator; 
        
        public T Aggregate(IEnumerable<T> values)
        {
            return values.Aggregate((left, right) => this.@operator.Execute(left, right));
        }
    }
}