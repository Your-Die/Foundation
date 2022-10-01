namespace Datastructures
{
    using System;
    using Chinchillada;
    using UnityEngine;

    [Serializable]
    public class Range
    {
        [SerializeField] private float minimum;
        [SerializeField] private float maximum;

        public Range(float minimum, float maximum)
        {
            this.minimum = minimum;
            this.maximum = maximum;
        }

        public float Minimum
        {
            get => this.minimum;
            set => this.minimum = value;
        }

        public float Maximum
        {
            get => this.maximum;
            set => this.maximum = value;
        }

        public float Clamp(float value) => Mathf.Clamp(value, this.minimum, this.maximum);
    }
}