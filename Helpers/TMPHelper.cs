namespace Chinchillada.Utilities
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public static class TMPHelper
    {
        public static string WrapColor(this string text, Color color)
        {
            const string colorTag = "color";
            var hex = ColorUtility.ToHtmlStringRGBA(color);
            var hexString = "#" + hex;

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
            return $"<{tagName}={tagContent}>";
        }

        public static string GetTagClose(string tagName) => $"</{tagName}>";

        public static bool TryGetLinkID(this TMP_Text text, Vector3 position, Camera camera, out string linkID)
        {
            var linkIndex = TMP_TextUtilities.FindIntersectingLink(text, position, camera);

            if (linkIndex == -1)
            {
                linkID = null;
                return false;
            }

            var linkInfo = text.textInfo.linkInfo[linkIndex];
            linkID = linkInfo.GetLinkID();
            return true;
        }
    }
}