using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Chinchillada.Utilities
{
    [Serializable]
    public abstract class PoolingListBase<TItem> : IReadOnlyList<TItem>
    {
        [SerializeField] private TItem prefab;
        [SerializeField] private Transform parent;

        [SerializeField, HideInInspector] private List<TItem> items = new List<TItem>();
        [SerializeField, HideInInspector] private Stack<TItem> unusedItems = new Stack<TItem>();

        public event Action<TItem> ItemAdded;
        public event Action<TItem> ItemDeactivated;

        public int Count => this.items.Count;

        protected TItem Prefab => this.prefab;

        protected Transform Parent => this.parent;

        public TItem this[int index]
        {
            get => this.items[index];
            set => this.items[index] = value;
        }

        public PoolingListBase()
        {
        }

        public PoolingListBase(TItem prefab, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;
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

        public void Apply<TOther>(TOther other, Action<TOther, TItem> action)
        {
            this.Scope(1);
            var item = this.items.First();

            action(other, item);
        }

        public void ForEach(Action<TItem> action)
        {
            foreach (var item in this.items)
            {
                action(item);
            }
        }

        public int Scope(int count)
        {
            var delta = 0;

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

                Deactivate(item);
                this.unusedItems.Push(item);

                delta--;
                this.ItemDeactivated?.Invoke(item);
            }

            return delta;
        }


        public void Clear()
        {
            this.Scope(0);
        }

        public TItem Acquire()
        {
            var item = this.unusedItems.Any()
                ? this.unusedItems.Pop()
                : this.CreateNew();

            Activate(item);
            this.items.Add(item);

            this.ItemAdded?.Invoke(item);
            return item;
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int IndexOf(TItem item) => this.items.IndexOf(item);
        
        protected abstract void Activate(TItem item);

        protected abstract void Deactivate(TItem item);

        protected abstract TItem CreateNew();
    }
}