using System;
using UnityEngine;

namespace Chinchillada
{
    public class RNGComponent : AutoRefBehaviour, IRNG
    {
        [SerializeField] private IRNG random;

        public void SetSeed(int seed) => this.random.SetSeed(seed);

        public float Float() => this.random.Float();

        public float Range(float min, float max) => this.random.Range(min, max);

        public int Range(int min, int max, bool inclusive = false) => this.random.Range(min, max, inclusive);

        public void Initialize() => this.random.Initialize();

        [Serializable]
        public class Reference : IRNG
        {
            [SerializeField] private RNGComponent component;

            public void Initialize() => this.component.Initialize();

            public void SetSeed(int seed) => this.component.SetSeed(seed);

            public float Float() => this.component.Float();

            public float Range(float min, float max) => this.component.Range(min, max);

            public int Range(int min, int max, bool inclusive = false) => this.component.Range(min, max, inclusive);
        }
    }
}