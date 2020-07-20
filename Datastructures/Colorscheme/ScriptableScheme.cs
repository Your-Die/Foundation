using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada.Colors
{
    public class ScriptableScheme : ScriptableObject, IColorScheme
    {
        [SerializeField] private ColorScheme scheme;

        public void CopyFrom(IColorScheme sourceScheme)
        {
            this.scheme = new ColorScheme(sourceScheme);
        }

        public IEnumerator<Color> GetEnumerator() => this.scheme.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public int Count => this.scheme.Count;

        public Color this[int index] => this.scheme[index];
    }
}