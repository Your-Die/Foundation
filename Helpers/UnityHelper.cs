using UnityEngine;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Static class containing helper functions for Unity objects.
    /// </summary>
    public static class UnityHelper
    {
        /// <summary>
        /// Tries to find an object of the given type, otherwise creates one.
        /// </summary>
        public static T FindOrCreate<T>() where T : Component
        {
            T instance = Object.FindObjectOfType<T>();
            return instance != null ? instance : CreateObject<T>();
        }

        /// <summary>
        /// Creates a <see cref="GameObject"/> with a component of the given type attached.
        /// </summary>
        public static T CreateObject<T>() where T : Component
        {
            var gameObject = new GameObject(typeof(T).Name);
            return gameObject.AddComponent<T>();
        }
    }
}
