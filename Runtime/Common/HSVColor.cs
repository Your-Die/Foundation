namespace Chinchillada
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct HSVColor
    {
        [SerializeField] private float hue;
        [SerializeField] private float saturation;
        [SerializeField] private float value;

        public float Hue
        {
            get => this.hue;
            set => this.hue = this.EnsureInRange(value);
        }

        public float Saturation
        {
            get => this.saturation;
            set => this.saturation = this.EnsureInRange(value);
        }

        public float Value
        {
            get => this.value;
            set => this.value = this.EnsureInRange(value);
        }

        public HSVColor(float hue, float saturation, float value)
        {
            this.hue        = hue;
            this.saturation = saturation;
            this.value      = value;

            this.EnsureInRange();
        }

        public HSVColor(Color color)
        {
            Color.RGBToHSV(color, out this.hue, out this.saturation, out this.value);
            this.EnsureInRange();
        }

        public HSVColor(HSVColor color) : this(color.Hue, color.Saturation, color.Value)
        {
        }

        public Color ToRGB() => Color.HSVToRGB(this.Hue, this.Saturation, this.Value);

        public override string ToString() => $"{this.Hue:F2}, {this.Saturation:F2}, {this.Value:F2}";

        private void EnsureInRange()
        {
            this.Hue = this.hue;
            this.Saturation = this.saturation;
            this.Value = this.value;
        }

        private float EnsureInRange(float floatingPoint)
        {
            return floatingPoint <= 1f ? floatingPoint : floatingPoint % 1f;
        }
    }
}