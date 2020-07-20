using System;
using System.Collections;
using System.Collections.Generic;
using Chinchillada.Colors;
using JetBrains.Annotations;
using UnityEngine;

[Serializable, UsedImplicitly]
public class ColorManagerReference : IColorScheme
{
    public int Count => ColorschemeManager.Instance.Count;

    public Color this[int index] => ColorschemeManager.Instance[index];
    
    public IEnumerator<Color> GetEnumerator() => ColorschemeManager.Instance.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
