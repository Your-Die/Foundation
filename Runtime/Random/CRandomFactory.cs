namespace Chinchillada.PCGraph
{
    using System;
    using UnityEngine;

    [Serializable]
    public class CRandomFactory : IFactory<CRandom>
    {
        [SerializeField] private int seed;

        public CRandom Create() => new CRandom(this.seed);
    }
}