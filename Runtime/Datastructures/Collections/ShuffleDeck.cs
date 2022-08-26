namespace Chinchillada
{
    using System.Collections.Generic;
    using System.Linq;

    public class ShuffleDeck<T>
    {
        private readonly List<T> drawPile    = new List<T>();
        private readonly List<T> discardPile = new List<T>();

        public int DrawCount    => this.drawPile.Count;
        public int DiscardCount => this.discardPile.Count;

        public ShuffleDeck(params T[] items) : this(items.AsEnumerable())
        {
        }

        public ShuffleDeck(IEnumerable<T> items)
        {
            this.drawPile.AddRange(items);
        }

        public T Draw(IRNG random)
        {
            if (this.drawPile.IsEmpty())
                this.ShuffleInDiscard();

            var index = this.drawPile.ChooseRandomIndex(random);
            var item  = this.DrawToDiscard(index);

            return item;
        }

        public void ShuffleInDiscard()
        {
            this.drawPile.AddRange(this.discardPile);
            this.discardPile.Clear();
        }

        private T DrawToDiscard(int index)
        {
            var item = this.drawPile.Extract(index);
            this.discardPile.Add(item);

            return item;
        }
    }
}