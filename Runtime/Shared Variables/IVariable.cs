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
    }

    [Serializable]
    public class ConstantVariable<T> : IVariable<T>
    {
        [SerializeField] private T value;

        public T Value
        {
            get => this.value;
            set => this.value = value;
        }
    }
}