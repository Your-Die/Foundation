namespace Chinchillada
{
    using System;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;
    using UnityEngine;
    using Random = System.Random;

    [Serializable]
    public class CRandom : IRNG ,IInitializable
    {
        [SerializeField] private bool useRandomSeed;

        [OdinSerialize]
        [ShowIf(nameof(useRandomSeed))]
        private IRandomSeedStrategy seedStrategy;
        
        [SerializeField]
        [HideIf(nameof(useRandomSeed))]
        private int seed;

        [ShowInInspector] [ReadOnly] private int runtimeSeed;
        
        private Random random;
        
        public void Initialize()
        {
            this.runtimeSeed = this.useRandomSeed
                ? this.seedStrategy.GenerateSeed()
                : this.seed;

            this.random = new Random(this.runtimeSeed);
        }

        public void SetSeed(int newSeed) => this.random = new Random(newSeed);

        public float Float() => (float) this.random.NextDouble();

        public float Range(float min, float max)
        {
            var range = max - min;
            return min + range * this.Float();
        }

        public int Range(int min, int max, bool inclusive = false)
        {
            if (inclusive) max++;
            return this.random.Next(min, max);
        }
    }
}