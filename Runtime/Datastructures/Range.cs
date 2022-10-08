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

        public Range()
        {
        }
        
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

        public float Size => this.maximum - this.minimum;

        public float Clamp(float        value) => Mathf.Clamp(value, this.minimum, this.maximum);

        public float InverseLerp(float value) => Mathf.InverseLerp(this.minimum, this.maximum, value);

        public void GrowWithPercentage(float percentage)
        {
            var increase     = this.Size * percentage;
            var halfIncrease = increase  / 2f;

            this.minimum -= halfIncrease;
            this.maximum += halfIncrease;
        }
        
        public override string ToString() => $"[{this.minimum}, {this.maximum}]";
    }
}