namespace Chinchillada
{
    using System;
    using UnityEngine;

    [Serializable]
    public class CRandomFactory : IFactory<SerializableRandom>
    {
        [SerializeField] private int seed;

        public SerializableRandom Create() => new SerializableRandom(this.seed);
    }
}