namespace Chinchillada.Foundation
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class ShuffleDeck<T>
    {
        [SerializeField] private List<T> items;


        public List<T> Items => this.items;

        public List<T> Discard { get ; } = new List<T>();

        public int Count => this.items.Count + this.Discard.Count;

        public ShuffleDeck() : this(new List<T>())
        {
        }

        public ShuffleDeck(List<T> items) 
        {
            this.items = items;
        }

        public T Draw()
        {
            if (this.Count == 0)
                return default;

            if (this.items.IsEmpty())
                this.ShuffleInDiscard();

            var index = this.items.ChooseRandomIndex();
            var item  = this.items.Extract(index);

            this.Discard.Add(item);

            return item;
        }

        public void ShuffleInDiscard()
        {
            this.items.AddRange(this.Discard);
            this.Discard.Clear();
        }
    }
}