using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada.Foundation
{
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
                this.itemQueue = new Queue<TItem>(this.items);

            if (this.itemQueue.IsEmpty())
                throw new IndexOutOfRangeException();

            return this.itemQueue.Dequeue();
        }
    }
}