using UnityEngine;

namespace Chinchillada.Utilities
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private T _instance;

        public T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = UnityHelper.FindOrCreate<T>();
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
                return;

            Debug.Log($"Duplicate singleton of type ({nameof(T)}) awoken. Destroying {this.name}");
            Destroy(gameObject);
        }
    }
}
