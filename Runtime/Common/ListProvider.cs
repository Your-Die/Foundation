namespace Chinchillada
{
    using System;
    using System.Collections.Generic;
    using Chinchillada;
    using UnityEngine;

    [Serializable]
    public class ListProvider<T> : IProvider<T>
    {
        [SerializeField] private List<T> items = new List<T>();
        
        public IEnumerable<T> Provide() => this.items;
    }
}