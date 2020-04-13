using UnityEngine;

namespace Utilities.Pooling
{
    [CreateAssetMenu(menuName = "Chinchillada/Pool")]
    public class ObjectPoolReference : ScriptableObject, IGameObjectPool
    {
        [SerializeField] private GameObjectPoolBase poolPrefab;

        private GameObjectPoolBase pool;
        
        public GameObject Instantiate(Vector3? position = null, Transform parent = null)
        {
            this.EnsurePool();
            return this.pool.Instantiate(position, parent);
        }

        public T Instantiate<T>(Vector3? position = null, Transform parent = null)
        {
            this.EnsurePool();
            return this.pool.Instantiate<T>(position, parent);
        }

        public void Return(GameObject obj)
        {
            this.EnsurePool();
            this.pool.Return(obj);
        }
        
        private void EnsurePool()
        {
            if (this.pool != null)
                return;

            var poolParent = PoolOfPools.Instance.transform;
            this.pool = Instantiate(this.poolPrefab, poolParent);
        }
    }
}