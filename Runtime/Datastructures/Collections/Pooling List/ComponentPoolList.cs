namespace Chinchillada
{
    using System;
    using UnityEngine;
    using Object = UnityEngine.Object;

    [Serializable]
    public class ComponentPoolList<TItem> : PoolListBase<TItem> where TItem : Component
    {
        [SerializeField] private TItem     prefab;
        [SerializeField] private Transform parent;

        protected TItem Prefab => this.prefab;

        protected Transform Parent => this.parent;

        public ComponentPoolList()
        {
        }

        public ComponentPoolList(TItem prefab, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;
        }
        
        protected override void Activate(TItem item) => item.gameObject.SetActive(true);

        protected override void Deactivate(TItem item) => item.gameObject.SetActive(false);

        protected override TItem CreateNew()
        {
            return Object.Instantiate(this.Prefab, this.Parent);
        }
    }
}