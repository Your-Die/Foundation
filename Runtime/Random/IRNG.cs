namespace Chinchillada.Foundation.RNG
{
    public interface IRNG
    {
        bool Flip(float probability);
        int Range(int min, int max, bool inclusive = false);
    }
}