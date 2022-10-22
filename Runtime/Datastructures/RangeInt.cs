namespace Chinchillada
{
    using System;
    using UnityEngine;

    [Serializable]
    public class RangeInt : IRange<int>
    {
        [SerializeField] private int minimum;
        [SerializeField] private int maximum;

        public RangeInt()
        {
        }

        public RangeInt(int minimum, int maximum)
        {
            this.minimum = minimum;
            this.maximum = maximum;
        }

        public int Minimum
        {
            get => this.minimum;
            set => this.minimum = value;
        }

        public int Maximum
        {
            get => this.maximum;
            set => this.maximum = value;
        }

        public int Size => this.maximum - this.minimum;

        public int Clamp(int value)
        {
            if (value < this.minimum)
                return this.minimum;

            if (value > this.maximum)
                return this.maximum;

            return value;
        }

        public int InverseLerp(int value)
        {
            return (int)Mathf.InverseLerp(this.minimum, this.maximum, value);
        }
    }
}