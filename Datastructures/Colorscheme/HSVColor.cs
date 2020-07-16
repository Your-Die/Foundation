using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Chinchillada.Colors
{
    [Serializable]
    public struct HSVColor
    {
        [SerializeField, Range(0, 1)] private float hue;
        [SerializeField, Range(0, 1)] private float saturation;
        [SerializeField, Range(0, 1)] private float value;

        public float Hue
        {
            get => this.hue;
            set => this.hue = value;
        }

        public float Saturation
        {
            get => this.saturation;
            set => this.saturation = value;
        }

        public float Value
        {
            get => this.value;
            set => this.value = value;
        }

        public Color ToRGB() => Color.HSVToRGB(this.hue, this.saturation, this.value);

        public static float RandomHue() => Random.value;
    }
}