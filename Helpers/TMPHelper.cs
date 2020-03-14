namespace Chinchillada.Utilities
{
    using UnityEngine;

    public static class TMPHelper
    {
        public static string WrapColor(this string text, Color color)
        {
            const string colorTag = "color";
            var hexString = ColorUtility.ToHtmlStringRGBA(color);
            
            return WrapTag(text, colorTag, hexString);
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

        public static string GetTagOpen(string tagName, string tagContent)
        {
            return $"<{tagName}=\"{tagContent}\">";
        }

        public static string GetTagClose(string tagName) => $"</{tagName}>";
    }
}