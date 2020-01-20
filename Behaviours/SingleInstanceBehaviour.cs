using Chinchillada;
using UnityEngine;

namespace Utilities.Behaviours
{
    public abstract class SingleInstanceBehaviour<T> : ChinchilladaBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        public static bool HasInstance => Instance != null;

        protected abstract T GetInstance();

        protected override void Awake()
        {
            if (HasInstance)
            {
                Debug.Log($"Duplicate single-object of type ({nameof(T)}) awoken. Destroying {this.name}");
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this.GetInstance();
                base.Awake();
            }
        }
    }
}