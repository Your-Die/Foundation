namespace Chinchillada.Foundation.RNG
{
    public class UnityRandom : IRNG
    {
        public bool Flip(float probability) => Random.value <= probability;
        public int Range(int min, int max, bool inclusive)
        {
            max = inclusive ? max + 1 : max;
            return Random.Range(min, max);
        }
    }
}