using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Chinchillada.Foundation
{
    [Serializable]
    public class UPoolingList<TItem> : ComponentPoolListBase<TItem> where TItem : Component
    {
        protected override void Activate(TItem item) => item.gameObject.SetActive(true);

        protected override void Deactivate(TItem item) => item.gameObject.SetActive(false);

        protected override TItem CreateNew()
        {
            var gameObject = Object.Instantiate(this.Prefab.gameObject, this.Parent);
            return gameObject.GetComponent<TItem>();
        }
    }
}