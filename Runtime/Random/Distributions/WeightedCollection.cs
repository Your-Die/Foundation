namespace Chinchillada
{
    using System.Collections.Generic;
    using System.Linq;

    public class WeightedCollection<T> : IDistribution<T>
    {
        private readonly IDictionary<T, float> weightedItems;

        public WeightedCollection(IDictionary<T, float> items)
        {
            this.weightedItems = items;
        }

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

    public class WeightedCollectionFactory : IDistributionFactory
    {
        public IDistribution<T> BuildDistribution<T>(IDictionary<T, float> items)
        {
            return new WeightedCollection<T>(items);
        }
    }
}