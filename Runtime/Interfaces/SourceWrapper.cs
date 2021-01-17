namespace Interfaces
{
    using System;
    using Chinchillada.Foundation;
    using UnityEngine;

    [Serializable]
    public class SourceWrapper<T> : ISource<T>
    {
        [SerializeField] private T item;

        public SourceWrapper() => this.item = default;
        
        public SourceWrapper(T item) => this.item = item;

        public T GetValue() => this.item;
    }
}