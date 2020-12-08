namespace Chinchillada.Foundation.RNG
{
    public interface IRNG
    {
        bool Flip(float probability = 0.5f);
        int Range(int min, int max, bool inclusive = false);
    }
}