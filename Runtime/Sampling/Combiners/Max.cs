using UnityEngine;

namespace Chinchillada.Sampling
{
    using System;

    [Serializable]
    public class Max : ISampleCombiner
    {
        public float Combine(float sampleX, float sampleY)
        {
            return Mathf.Max(sampleX, sampleY);
        }
    }
}