namespace Chinchillada
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using Random = System.Random;

    [Serializable]
    public class SerializableRandom : IRNG, IInitializable, ISerializationCallbackReceiver
    {
        [SerializeField] private bool useRandomSeed;

        [SerializeReference]
        [ShowIf(nameof(useRandomSeed))]
        private IRandomSeedStrategy seedStrategy = new FrameCountSeed();

        [SerializeField]
        [HideIf(nameof(useRandomSeed))]
        private int seed;

        private Random random;

        [ShowInInspector] [ReadOnly] private int runtimeSeed;

        public SerializableRandom(int seed)
        {
            this.seed          = seed;
            this.useRandomSeed = false;
        }


        [Button]
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

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize() => this.Initialize();
    }
}