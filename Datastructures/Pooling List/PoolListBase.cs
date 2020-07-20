using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Chinchillada.Foundation
{
    [Serializable]
    public abstract class PoolListBase<TItem> : IReadOnlyList<TItem>
    {
        private List<TItem> items = new List<TItem>();
        private Stack<TItem> unusedItems = new Stack<TItem>();

        public event Action<TItem> ItemAdded;
        public event Action<TItem> ItemDeactivated;

        public int Count => this.items.Count;

        public TItem this[int index]
        {
            get => this.items[index];
            set => this.items[index] = value;
        }

        public int ApplyWith<TOther>(IList<TOther> list, Action<TOther, TItem> action)
        {
            var count = list.Count;
            var delta = this.Scope(count);

            for (var i = 0; i < list.Count; i++)
            {
                var other = list[i];
                var item = this.items[i];

                action(other, item);
            }

            return delta;
        }

        public int ApplyWith<TOther>(IList<TOther> list, Action<int, TOther, TItem> action)
        {
            var count = list.Count;
            var delta = this.Scope(count);

            for (var index = 0; index < list.Count; index++)
            {
                var other = list[index];
                var item = this.items[index];

                action(index, other, item);
            }

            return delta;
        }

        public void Apply<TOther>(TOther other, Action<TOther, TItem> action)
        {
            this.Scope(1);
            var item = this.items.First();

            action(other, item);
        }


        public int Scope(int count)
        {
            var delta = 0;

            if (this.items == null)
                this.items = new List<TItem>();

            while (this.items.Count < count)
            {
                delta++;
                this.Acquire();
            }

            while (this.items.Count > count)
            {
                var item = this.items.ExtractLast();

                if (item is IPoolable poolable)
                    poolable.OnRelease();

                this.Deactivate(item);
                this.unusedItems.Push(item);

                delta--;
                this.ItemDeactivated?.Invoke(item);
            }

            return delta;
        }

        [Button]
        public void AddEmptyItem()
        {
            var currentCount = this.items?.Count ?? 0;
            this.Scope(currentCount + 1);
        }

        public void Clear()
        {
            this.Scope(0);
        }

        public TItem Acquire()
        {
            if (this.unusedItems == null)
                this.unusedItems = new Stack<TItem>();

            var item = this.unusedItems.Any()
                ? this.unusedItems.Pop()
                : this.CreateNew();

            this.Activate(item);
            this.items.Add(item);

            this.ItemAdded?.Invoke(item);
            return item;
        }

        public IEnumerator<TItem> GetEnumerator() => this.items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public int IndexOf(TItem item) => this.items.IndexOf(item);

        protected virtual void Activate(TItem item)
        {
        }

        protected virtual void Deactivate(TItem item)
        {
        }

        protected abstract TItem CreateNew();
    }
}