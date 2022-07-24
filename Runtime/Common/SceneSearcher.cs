namespace Chinchillada
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    
    [Serializable]
    public class SceneSearcher<T> : IProvider<T>
    {
        [SerializeField] private SearchStrategy strategy;

        [SerializeField] private bool includeInactive = true;

        [SerializeField] private GameObject gameObject;
        
        public IEnumerable<T> Provide()
        {
            return this.strategy.FindComponents<T>(this.gameObject, this.includeInactive);
        }
    }
}