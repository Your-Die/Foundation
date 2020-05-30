using System;
using System.Collections.Generic;
using System.Linq;
using Chinchillada.Foundation;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Chinchillada.Foundation.UI
{
    [Serializable]
    public class TextStyle
    {
        [SerializeField] private Color color = Color.black;

        [SerializeField] private bool bold;
        [SerializeField] private bool italic;
        [SerializeField] private bool strikethrough;
        [SerializeField] private bool underline;

        [SerializeField, HorizontalGroup("Spacing")]
        private bool useSpacing;

        [SerializeField, HorizontalGroup("Spacing"), ShowIf(nameof(useSpacing))]
        private float spacing;

        [SerializeField, HorizontalGroup("Font")]
        private bool overrideFont;

        [SerializeField, HorizontalGroup("Font"), ShowIf(nameof(overrideFont))]
        private TMP_FontAsset font;

        [SerializeField, EnumToggleButtons] 
        private CapsStyle capsStyle = CapsStyle.None;

        [SerializeField, HorizontalGroup("Mark")]
        private bool useMark;

        [SerializeField, HorizontalGroup("Mark"), ShowIf(nameof(useMark))]
        private Color markColor;

        [SerializeField, HorizontalGroup("Size")]
        private bool overrideSize;

        [SerializeField, HorizontalGroup("Size"), ShowIf(nameof(overrideSize))]
        [Range(0, 200)]
        private int size = 100;

        
        public string Apply(string text)
        {
            var modifiers = this.GetModifiers();
            return modifiers.Aggregate(text, (current, modifier) => modifier.Invoke(current));
        }

        private IEnumerable<Func<string, string>> GetModifiers()
        {
            yield return text => text.WrapColor(this.color);

            if (this.bold)
                yield return text => text.WrapBold();

            if (this.italic)
                yield return text => text.WrapItalic();

            if (this.useSpacing)
                yield return text => text.WrapSpacing(this.spacing);

            if (this.overrideFont)
                yield return text => text.WrapFont(this.font);

            yield return text => text.WrapCaps(this.capsStyle);

            if (this.useMark)
                yield return text => text.WrapMark(this.markColor);

            if (this.overrideSize)
                yield return text => text.WrapSize(this.size);

            if (this.strikethrough)
                yield return text => text.WrapStrikethrough();

            if (this.underline)
                yield return text => text.WrapUnderline();
        }
    }
}