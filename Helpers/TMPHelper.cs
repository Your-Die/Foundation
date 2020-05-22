namespace Chinchillada.Foundation
{
    using System;
    using TMPro;
    using UnityEngine;

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

        public static int GetMouseoverLinkIndex(this TMP_Text text)
        {
            var position = Input.mousePosition;
            var camera = Camera.main;

            if (camera == null)
                throw new InvalidOperationException();

             return TMP_TextUtilities.FindIntersectingLink(text, position, camera);
        }

        public static void SetColor(this TMP_Text text, TMP_LinkInfo linkInfo, Color32 color,
            bool updateVertexData = true)
        {
            var textInfo = text.textInfo;

            for (var i = 0; i < linkInfo.linkTextLength; i++)
            {
                var characterIndex = linkInfo.linkTextfirstCharacterIndex + i;

                // Get the index of the material / sub text object used by this character.
                var meshIndex = textInfo.characterInfo[characterIndex].materialReferenceIndex;

                // Get the index of the first vertex of this character.
                var vertexIndex = textInfo.characterInfo[characterIndex].vertexIndex;

                // Get a reference to the vertex color
                var vertexColors = textInfo.meshInfo[meshIndex].colors32;

                vertexColors[vertexIndex + 0] = color;
                vertexColors[vertexIndex + 1] = color;
                vertexColors[vertexIndex + 2] = color;
                vertexColors[vertexIndex + 3] = color;
            }

            if (updateVertexData)
                text.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
        }
    }
}