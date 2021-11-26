namespace Chinchillada
{
    using System;
    using UnityEngine;

    public interface IVariable<T>
    {
        /// <summary>
        /// The value.
        /// </summary>
        T Value { get; set; }

        /// <summary>
        /// Event invoked when the value is changed.
        /// </summary>
        event Action<T> ValueChanged;
    }

    [Serializable]
    public class ConstantVariable<T> : IVariable<T>
    {
        [SerializeField] private T value;

        public T Value
        {
            get => this.value;
            set
            {
                if (this.value.Equals(value))
                    return;

                this.value = value;
                this.ValueChanged?.Invoke(this.value);
            }
        }

        public event Action<T> ValueChanged;
    }
}