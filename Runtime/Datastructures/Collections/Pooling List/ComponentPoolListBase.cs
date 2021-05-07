using System;
using UnityEngine;

namespace Chinchillada
{
    [Serializable]
    public abstract class ComponentPoolListBase<TItem> : PoolListBase<TItem>
    {
        [SerializeField] private TItem prefab;
        [SerializeField] private Transform parent;

        protected TItem Prefab => this.prefab;

        protected Transform Parent => this.parent;

        protected ComponentPoolListBase()
        {
        }

        protected ComponentPoolListBase(TItem prefab, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;
        }
    }
}