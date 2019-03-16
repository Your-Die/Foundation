using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// A variable of the given type that is wrapped in a <see cref="ScriptableObject"/> so it can be easily shared by different systems
    /// in a modular way.
    /// </summary> 
    public abstract class SharedVariable<T> : ScriptableObject, 
        ISerializationCallbackReceiver where T : IComparable
    {
        [SerializeField] private T _initialValue;

        /// <summary>
        /// The value during runtime.
        /// </summary>
        private T _runtimeValue;
        
        /// <summary>
        /// Event invoked when the value is changed.
        /// </summary>
        public event Action<T> ValueChanged;

        /// <summary>
        /// The value. 
        /// </summary>
        public T Value
        {
            get => _runtimeValue;
            set
            {
                if (_runtimeValue.CompareTo(value) != 0)
                    return;

                _runtimeValue = value;
                ValueChanged?.Invoke(value);
            }
        }

        /// <summary>
        /// Resets the variable to the initial value.
        /// </summary>
        public void OnBeforeSerialize()
        {
            Value = _initialValue;
        }

        public void OnAfterDeserialize()
        {
        }

        [Button]
        public void SaveCurrentValueAfterPlay()
        {
            _initialValue = _runtimeValue;
        }
    }
}