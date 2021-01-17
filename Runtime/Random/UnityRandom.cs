namespace Chinchillada
{
    using System;
    using Foundation;
    using Random = UnityEngine.Random;

    [Serializable]
    public class UnityRandom : IRNG
    {
        public static readonly UnityRandom Shared = new UnityRandom();
        
        public void Initialize() { }

        public void SetSeed(int seed) => Random.InitState(seed);

        public float Float() => Random.value;

        public float Range(float min, float max)
        {
            return Random.Range(min, max);
        }

        public int Range(int min, int max, bool inclusive)
        {
            max = inclusive ? max + 1 : max;
            return Random.Range(min, max);
        }
    }
}