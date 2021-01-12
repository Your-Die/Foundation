namespace Chinchillada.Sampling
{
    public interface ISampleModifier
    {
        float Process(float sample, float percentage);
    }
}