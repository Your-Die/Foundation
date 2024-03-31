namespace Chinchillada
{
    public class WeightedItem<T>
    {
        public T Item { get; }

        public float Weight { get; }

        public WeightedItem(T item, float weight)
        {
            this.Item = item;
            this.Weight = weight;
        }

        public override string ToString() => $"{this.Item} - {this.Weight}";
    }
}