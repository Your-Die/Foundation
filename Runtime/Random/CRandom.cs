using System;

namespace Chinchillada
{
    public class CRandom : IRNG
    {
        private Random random;
        
        public CRandom(int seed)
        {
            SetSeed(seed);
        }

        public void Initialize()
        {
        }

        public void SetSeed(int seed) => this.random = new Random(seed);

        public float Float()
        {
            return (float)this.random.NextDouble();
        }

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