namespace Chinchillada
{
    using System;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;
    using UnityEngine;

    [Serializable]
    public class DelegateContainer<T> :  IDelegateContainer<T>
    {
        [SerializeField] private T value;

        [OdinSerialize] private ISource<T> source;
        
        public T Get()
        {
            return this.value ??= this.source.Get();
        }

        public void Set(T newValue)
        {
            this.value = newValue;
        }

        [Button]
        public void Refresh() => this.value = this.source.Get();
    }
}