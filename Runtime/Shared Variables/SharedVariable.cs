using System;
using Chinchillada;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chinchillada
{
    /// <summary>
    /// A variable of the given type that is wrapped in a <see cref="ScriptableObject"/> so it can be easily shared by different systems
    /// in a modular way.
    /// </summary> 
    public abstract class SharedVariable<T> : ScriptableObject, IListenable<T>, IContainer<T>,
                                              ISerializationCallbackReceiver
        where T : IComparable, IEquatable<T>
    {
        [FormerlySerializedAs("_initialValue")] [SerializeField]
        private T initialValue;

        /// <summary>
        /// The value during runtime.
        /// </summary>
        private T runtimeValue;

        /// <summary>
        /// Event invoked when the value is changed.
        /// </summary>
        public event Action<T> ValueChanged;

        /// <summary>
        /// The value. 
        /// </summary>
        public T Value
        {
            get => this.runtimeValue;
            set
            {
                if (this.runtimeValue.CompareTo(value) != 0)
                    return;

                this.runtimeValue = value;
                this.ValueChanged?.Invoke(value);
            }
        }
        
        /// <summary>
        /// Resets the variable to the initial value.
        /// </summary>
        public void OnBeforeSerialize() => this.Value = this.initialValue;

        public void OnAfterDeserialize()
        {
        }

        /// <summary>
        /// Will cause the current value to be used as the new value outside of play-mode.
        /// </summary>
        [Button]
        public void SaveCurrentValueAfterPlay() => this.initialValue = this.runtimeValue;

        public static implicit operator T(SharedVariable<T> variable) => variable.Value;
        
        T ISource<T>.Get() => this.Value;
        void IContainer<T>.Set(T value) => this.Value = value;

    }
}