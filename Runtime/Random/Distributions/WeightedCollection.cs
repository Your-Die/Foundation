namespace Chinchillada
{
    using System.Collections.Generic;
    using System.Linq;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    public class WeightedCollection<T> : IDistribution<T>
    {
        [OdinSerialize, Required] private Dictionary<T, float> weightedItems = new Dictionary<T, float>(); 
        
        public T Sample(IRNG random)
        {
            var weightTotal = this.weightedItems.Values.Sum();

            var price = random.Range(0, weightTotal);

            var items = this.weightedItems.Keys.RandomOrder(random).ToArray();
            
            foreach (var item in items)
            {
                var weight = this.weightedItems[item];
                price -= weight;

                if (price <= 0)
                {
                    return item;
                }
            }

            return items.Last();
        }
    }
}