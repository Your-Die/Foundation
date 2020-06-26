namespace Chinchillada.Foundation
{
    using System;
    using TMPro;
    using UnityEngine;
    
    public enum CapsStyle
    {
        None,
        Uppercase,
        Lowercase,
        SmallCaps
    }

    public static class TMPHelper
    {
        public static string WrapColor(this string text, Color color)
        {
            const string colorTag = "color";
            var content = GetColorString(color);

            return WrapTag(text, colorTag, content);
        }

        public static string WrapBold(this string text)
        {
            const string bold = "b";
            return WrapTag(text, bold);
        }    
        
        public static string WrapItalic(this string text)
        {
            const string tag = "i";
            return WrapTag(text, tag);
        }     
        
        public static string WrapStrikethrough(this string text)
        {
            const string tag = "s";
            return WrapTag(text, tag);
        }

        public static string WrapStyle(this string text, string styleName)
        {
            const string tag = "style";
            return WrapTag(text, tag, styleName);
        }
        
        public static string WrapUnderline(this string text)
        {
            const string tag = "u";
            return WrapTag(text, tag);
        }
        
        public static string WrapSpacing(this string text, float spacing)
        {
            const string tag = "cspace";
            var content = $"{spacing}em";

            return WrapTag(text, tag, content);
        }

        public static string WrapFont(this string text, TMP_FontAsset font)
        {
            const string tag = "font";
            var content = $"\"{font.name}\"";

            return WrapTag(text, tag, content);
        }

        public static string WrapIndent(this string text, int indentPercentage)
        {
            const string tag = "indent";
            var content = $"{indentPercentage}%";

            return WrapTag(text, tag, content);
        }

        public static string WrapCaps(this string text, CapsStyle style)
        {
            if (style == CapsStyle.None)
                return text;

            var tag = ChooseTag();
            return WrapTag(text, tag);

            string ChooseTag()
            {
                switch (style)
                {
                    case CapsStyle.Uppercase:
                        return "uppercase";
                    case CapsStyle.Lowercase:
                        return "lowercase";
                    case CapsStyle.SmallCaps:
                        return "smallcaps";
                    default:
                        throw new ArgumentOutOfRangeException(nameof(style), style, null);
                }
            }
        }

        public static string WrapSize(this string text, int size)
        {
            const string tag = "size";
            var content = $"{size}%";

            return WrapTag(text, tag, content);
        }

        public static string WrapMark(this string text, Color color)
        {
            const string tag = "mark";
            var content = GetColorString(color);

            return WrapTag(text, tag, content);
        }

        public static string WrapLink(this string text, string linkID)
        {
            const string tag = "link";
            return WrapTag(text, tag, linkID);
        }

        public static string GetLinkOpen(string linkID)
        {
            const string tag = "link";
            return GetTagOpen(tag, linkID);
        }

        public static string GetLinkClose()
        {
            const string tag = "link";
            return GetTagClose(tag);
        }

        public static string WrapTag(this string text, string tagName, string tagContent)
        {
            var tagOpen = GetTagOpen(tagName, tagContent);
            var tagClose = GetTagClose(tagName);

            return $"{tagOpen}{text}{tagClose}";
        }

        public static string WrapTag(this string text, string tag)
        {
            var tagOpen = GetTagOpen(tag);
            var tagClose = GetTagClose(tag);

            return $"{tagOpen}{text}{tagClose}";
        }

        public static string GetTagOpen(string tagName, string tagContent = null)
        {
            return tagContent != null
                ? $"<{tagName}={tagContent}>"
                : $"<{tagName}>";
        }

        public static string GetTagClose(string tagName) => $"</{tagName}>";

        private static string GetColorString(Color color)
        {
            var hex = ColorUtility.ToHtmlStringRGBA(color);
            return "#" + hex;
        }
    }
}