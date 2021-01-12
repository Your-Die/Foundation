using UnityEngine;

namespace Chinchillada.Sampling
{
    using System;

    [Serializable]
    public class Min : ISampleCombiner
    {
        public float Combine(float sampleX, float sampleY)
        {
            return Mathf.Min(sampleX, sampleY);
        }
    }
}