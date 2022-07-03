namespace Chinchillada
{
    using System;
    using UnityEngine;

    public interface IVariable
    {
        /// <summary>
        /// Event invoked when the value is changed.
        /// </summary>
        event Action ValueChanged;
    }

    public interface IVariable<T> : IVariable
    {
        /// <summary>
        /// The value.
        /// </summary>
        T Value { get; set; }
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
                this.ValueChanged?.Invoke();
            }
        }

        public event Action ValueChanged;
    }
}