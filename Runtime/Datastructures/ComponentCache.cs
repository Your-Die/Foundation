using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada
{
    public class ComponentCache
    {
        private readonly GameObject gameObject;
        
        private Dictionary<Query, object> cache = new Dictionary<Query, object>();
        
        public ComponentCache(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public T FindComponent<T>(SearchStrategy strategy = SearchStrategy.InChildren)
        {
            var query = new Query
            {
                Type = typeof(T),
                Strategy = strategy
            };

            if (this.cache.TryGetValue(query, out var result))
                return (T) result;

            var component = query.Execute<T>(this.gameObject);
            this.cache[query] = component;

            return component;
        }
        
        private struct Query
        {
            public Type Type { get; set; }
            public SearchStrategy Strategy { get; set; }

            public T Execute<T>(GameObject gameObject) => this.Strategy.FindComponent<T>(gameObject);
        }
    }
}