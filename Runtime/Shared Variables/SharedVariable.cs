using System;
using Chinchillada.Foundation;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// A variable of the given type that is wrapped in a <see cref="ScriptableObject"/> so it can be easily shared by different systems
    /// in a modular way.
    /// </summary> 
    public abstract class SharedVariable<T> : ScriptableObject, IListenable<T>,
        ISource<T>,
        ISerializationCallbackReceiver where T : IEquatable<T>
    {
        [FormerlySerializedAs("_initialValue")]
        [SerializeField]
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
        [ShowInInspector]
        public T Value
        {
            get => this.runtimeValue;
            set
            {
                if (this.runtimeValue != null &&
                    this.runtimeValue.Equals(value))
                    return;

                this.runtimeValue = value;
                this.ValueChanged?.Invoke(value);
            }
        }

        public T GetValue() => this.Value;

        /// <summary>
        /// Resets the variable to the initial value.
        /// </summary>
        public void OnBeforeSerialize()
        {
            if (!Application.isPlaying)
            {
                this.Value = this.Copy(this.initialValue);
            }
        }

        public void OnAfterDeserialize()
        {
        }

        /// <summary>
        /// Will cause the current value to be used as the new value outside of play-mode.
        /// </summary>
        [Button]
        public void SaveCurrentValueAfterPlay() =>
            this.initialValue = this.Copy(this.runtimeValue);

        public static implicit operator T(SharedVariable<T> variable) =>
            variable.Value;

        protected virtual T Copy(T value) => value;
    }
}