namespace Chinchillada
{
    using Foundation;
    using UnityEngine;

    public class RNGComponent : ChinchilladaBehaviour, IRNG
    {
        [SerializeField] private IRNG random;

        public void SetSeed(int seed) => this.random.SetSeed(seed);

        public float Float() => this.random.Float();

        public float Range(float min, float max) => this.random.Range(min, max);

        public int Range(int min, int max, bool inclusive = false) => this.random.Range(min, max, inclusive);

        public void Initialize() => this.random.Initialize();
    }
}