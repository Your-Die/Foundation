using UnityEngine;

namespace Chinchillada.Utilities
{
    public static class UnityHelper
    {
        public static T FindOrCreate<T>() where T : Component
        {
            T instance = Object.FindObjectOfType<T>();
            return instance != null ? instance : CreateObject<T>();
        }

        public static T CreateObject<T>() where T : Component
        {
            GameObject gameObject = new GameObject(nameof(T));
            return gameObject.AddComponent<T>();
        }
    }
}
