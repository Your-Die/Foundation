﻿using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// Wrapper class for <see cref="MonoBehaviour"/> that creates a singleton of the wrapped type.
    /// </summary>
    /// <typeparam name="T">The type deriving from <see cref="MonoBehaviour"/> that this class wraps as singleton.</typeparam>
    public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static T instance;

        /// <summary>
        /// The singleton instance.
        /// </summary>
        public static T Instance
        {
            get
            {
                // Try to get existing instance.
                if (instance != null)
                    return instance;

                // Ensure existence.
                instance = UnityHelper.FindOrCreate<T>();
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
                return;

            // Destroy duplicates of singleton.
            Debug.Log($"Duplicate singleton of type ({nameof(T)}) awoken. Destroying {this.name}");
            Destroy(this.gameObject);
        }
    }
}
