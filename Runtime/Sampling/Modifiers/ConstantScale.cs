namespace Chinchillada.Sampling
{
    using System;
    using UnityEngine;

    [Serializable]
    public class ConstantScale : ISampleModifier
    {
        [SerializeField] [Range(0, 1)] private float scale;

        public float Process(float sample, float percentage)
        {
            return sample * this.scale;
        }
    }
}