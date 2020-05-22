using System;
using Chinchillada;
using Chinchillada.Foundation;
using Object = UnityEngine.Object;

[Serializable]
public class PoolingList<TItem> : ComponentPoolListBase<TItem> where TItem : IComponent
{
    protected override void Activate(TItem item) => item.gameObject.SetActive(true);

    protected override void Deactivate(TItem item) => item.gameObject.SetActive(false);

    protected override TItem CreateNew()
    {
        var gameObject = Object.Instantiate(this.Prefab.gameObject, this.Parent);
        return gameObject.GetComponent<TItem>();
    }
}