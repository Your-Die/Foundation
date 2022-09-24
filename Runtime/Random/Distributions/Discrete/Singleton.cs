namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class Singleton<T> : IDiscreteDistribution<T>
    {
        [SerializeField] private T singleton;

        private Singleton(T item) => this.singleton = item;
        public static Singleton<T> Distribution(T item) => new Singleton<T>(item);

        public T Sample(IRNG _) => this.singleton;

        public IEnumerable<T> Support()
        {
            yield return this.singleton;
        }

        public int Weight(T item) => item.Equals(this.singleton) ? 1 : 0;
    }
}