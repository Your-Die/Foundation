using Chinchillada;
using Chinchillada.Utilities;
using UnityEngine;

namespace Utilities.Behaviours
{
    /// <summary>
    /// Enforces that only a single instance of the <typeparamref name="T"/> exists.
    /// </summary>
    /// <remarks>
    /// Similar to <see cref="SingletonBehaviour{T}"/>, but does not create an instance if the instance is requested but
    /// none currently exists.
    /// </remarks>
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