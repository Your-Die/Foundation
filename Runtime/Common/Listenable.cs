using System;
using UnityEngine;

namespace Chinchillada.Common
{
    [Serializable]
    public class Listenable<T> where T : IEquatable<T>
    {
        [SerializeField] private T currentValue;

        public Listenable() => this.currentValue = default;

        public Listenable(T value) => this.currentValue= value;

        public event Action<T> ValueChanged;
        
        public T Value
        {
            get => this.currentValue;
            set
            {
                if (this.currentValue.Equals(value))
                    return;

                this.currentValue = value;
                this.ValueChanged?.Invoke(this.currentValue);
            }
        }
        
        public static implicit operator T(Listenable<T> listenable) => listenable.currentValue;
    }
}