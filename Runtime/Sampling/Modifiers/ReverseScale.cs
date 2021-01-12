namespace Chinchillada.Sampling
{
    using System;

    [Serializable]
    public class ReverseScale : ISampleModifier
    {
        public float Process(float sample, float percentage)
        {
            return (1 - percentage) * sample;
        }
    }
}