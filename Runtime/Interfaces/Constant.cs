using UnityEngine;

namespace Interfaces
{
    using System;
    using Chinchillada;
    using Sirenix.Serialization;

    [Serializable]
    public class Constant<T> : ISource<T>
    {
        [SerializeReference] private T item;

        public Constant() => this.item = default;
        
        public Constant(T item) => this.item = item;

        public T Get() => this.item;
    }
}