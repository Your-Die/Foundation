namespace Chinchillada.Sampling
{
    using System;

    [Serializable]
    public class Multiply : ISampleCombiner
    {
        public float Combine(float sampleX, float sampleY)
        {
            return sampleX * sampleY;
        }
    }
}