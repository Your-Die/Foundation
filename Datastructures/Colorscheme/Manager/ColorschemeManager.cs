using System.Collections;
using System.Collections.Generic;
using Chinchillada.Foundation;
using UnityEngine;

namespace Chinchillada.Colors
{
    public class ColorschemeManager : SingleInstanceBehaviour<ColorschemeManager>, IColorScheme, IColorschemeUser
    {
        [SerializeField] private IColorScheme colorScheme;
        
        public IColorScheme ColorScheme
        {
            get => colorScheme;
            set => colorScheme = value;
        }

        public int Count => this.colorScheme.Count;

        public Color this[int index] => this.colorScheme[index];

        public IColorScheme Scheme
        {
            get => this.colorScheme;
            set => this.colorScheme = value;
        }

        public IEnumerator<Color> GetEnumerator() => this.colorScheme.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}