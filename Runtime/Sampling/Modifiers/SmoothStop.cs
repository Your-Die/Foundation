namespace Chinchillada.Sampling
{
    using System;
    using Foundation;
    using UnityEngine;

    [Serializable]
    public class SmoothStop : ISampleModifier
    {
        [SerializeField] private int power = 2;
        
        public float Process(float sample, float _) => 1 - MathHelper.Power(1 - sample, this.power);
    }
}