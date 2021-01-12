namespace Chinchillada.Sampling
{
    using System;

    [Serializable]
    public class Scale : ISampleModifier
    {
        public float Process(float sample, float percentage) => sample * percentage;
    }
}