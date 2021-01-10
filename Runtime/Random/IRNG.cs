namespace Chinchillada.Foundation.RNG
{
    public interface IRNG
    {
        bool Flip(float probability = 0.5f);
        int Range(int min, int max, bool inclusive = false);
    }

    public static class RNGExtensions
    {
        public static int Range(this IRNG rng, int max, bool inclusive = false)
        {
            return rng.Range(0, max, inclusive);
        }
    }
}