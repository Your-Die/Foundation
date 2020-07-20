using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada.Colors
{
    [Serializable]
    public class ColorScheme : IColorScheme
    {
        [SerializeField] private Color[] colors;

        public int Count => this.colors.Length;

        public Color this[int index] => this.colors[index];

        public ColorScheme()
        {
        }

        public ColorScheme(params Color[] colors) => this.colors = colors;

        public ColorScheme(IEnumerable<Color> colors) => this.colors = colors.ToArray();

        public ColorScheme(IEnumerable<HSVColor> hsvColors)
        {
            this.colors = hsvColors.Select(hsv => hsv.ToRGB()).ToArray();
        }

        public IEnumerator<Color> GetEnumerator()
        {
            return ((IEnumerable<Color>) this.colors).GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public static ColorScheme operator +(ColorScheme x, ColorScheme y)
        {
            var mergedColors = x.colors.Concat(y.colors);
            return new ColorScheme(mergedColors);
        }
    }
}