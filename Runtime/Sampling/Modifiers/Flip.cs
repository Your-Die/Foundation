namespace Chinchillada.Sampling
{
    using System;

    [Serializable]
    public class Flip : ISampleModifier
    {
        public float Process(float sample, float percentage)
        {
            return 1 - sample;
        }
    }
}