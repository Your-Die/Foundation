using UnityEngine;

namespace Chinchillada
{
    public abstract class SingleInstanceBehaviour<T> : AutoRefBehaviour where T : SingleInstanceBehaviour<T>
    {
        public static T Instance { get; private set; }

        public static bool HasInstance => Instance != null;

        protected override void Awake()
        {
            if (HasInstance)
            {
                Debug.Log($"Duplicate single-object of type ({nameof(T)}) awoken. Destroying {this.name}");
                Destroy(this.gameObject);
            }
            else
            {
                Instance = (T) this;
                base.Awake();
                this.OnAwake();
            }
        }

        protected virtual void OnAwake()
        {
        }
    }
}