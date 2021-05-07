using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada
{
    /// <summary>
    /// Implementation of <see cref="PoolListBase{TItem}"/> that uses a list of existing objects.
    /// The objects are deactivated until they are acquired.
    /// </summary>
    [Serializable]
    public class EnablingPoolList<TItem> : PoolListBase<TItem> where TItem : Component
    {
        [SerializeField] private List<TItem> items;

        private Queue<TItem> itemQueue;
        
        protected override void Activate(TItem item) => item.gameObject.SetActive(true);

        protected override void Deactivate(TItem item) => item.gameObject.SetActive(false);
        
        protected override TItem CreateNew()
        {
            if (this.itemQueue == null) 
                this.Initialize();

            if (this.itemQueue.IsEmpty())
                throw new IndexOutOfRangeException();

            return this.itemQueue.Dequeue();
        }

        private void Initialize()
        {
            this.itemQueue = new Queue<TItem>(this.items);
            this.items.ForEach(this.Deactivate);
        }
    }
}