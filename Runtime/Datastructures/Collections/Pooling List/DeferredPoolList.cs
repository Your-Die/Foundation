namespace Chinchillada.Foundation
{
    using UnityEngine;

    public class DeferredPoolList<T> : PoolListBase<T>
    {
        [SerializeField] private IGameObjectPool pool;
        
        protected override T CreateNew() => this.pool.Instantiate<T>();
    }
}