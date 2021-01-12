namespace Chinchillada.Sampling
{
    using System;
    using Chinchillada;
    using Sampling;
    using UnityEngine;

    [Serializable]
    public class Mix : ISampleCombiner
    {
        [SerializeField] private ValueMixer mixer;

        public float Combine(float sampleX, float sampleY)
        {
            return this.mixer.Mix(sampleX, sampleY);
        }
    }
}