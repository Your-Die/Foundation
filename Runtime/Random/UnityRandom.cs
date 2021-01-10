namespace Chinchillada.Foundation.RNG
{
    using System;
    using Random = Foundation.Random;

    [Serializable]
    public class UnityRandom : IRNG
    {
        public static UnityRandom Instance = new UnityRandom();

        private UnityRandom()
        {
        }
        
        public bool Flip(float probability) => Random.value <= probability;
        public int Range(int min, int max, bool inclusive)
        {
            max = inclusive ? max + 1 : max;
            return Random.Range(min, max);
        }
    }
}