using System;
using UnityEngine;

namespace Chinchillada
{
    [Serializable]
    public class WeightedItem<T>
    {
        [SerializeField] private T item;

        [SerializeField] private int weight;

        public T   Item   => this.item;
        public int Weight => this.weight;

        public WeightedItem()
        {
        }

        public WeightedItem(T item, int weight = 1)
        {
            this.item = item;
            this.weight = weight;
        }

        public void Deconstruct(out T item, out int weight)
        {
            item = this.item;
            weight = this.weight;
        }

        public override string ToString() => $"{this.item}, w:{this.weight}";
    }
}