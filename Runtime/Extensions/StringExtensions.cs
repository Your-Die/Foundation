using System;
using System.Collections.Generic;

namespace Chinchillada.Foundation
{
    public static class StringExtensions
    {
        public static bool CanSubstringsBeFoundInOrder(this IEnumerable<string> subStrings,
                                                       string text,
                                                       StringComparison comparison = StringComparison.InvariantCulture)
        {
            int lastIndex = -1;

            foreach (string subString in subStrings)
            {
                int startIndex = lastIndex == -1 ? 0 : lastIndex + 1;
                int index = text.IndexOf(subString, startIndex, comparison);
                if (index == -1)
                    return false;

                lastIndex = index;
            }

            return true;
        }
        
        public static string GetTextBetween(this string text, int from, int to)
        {
            int length = to - from;
            return text.Substring(from, length);
        }

        public static string WrapColorTag(this string text, string color)
        {
            return text.WrapWithTagParameterized("color", color);
        }

        public static string WrapWithTagParameterized(this string text, string tag, string parameter)
        {
            return text.Wrap($"<{tag}={parameter}>", $"</{tag}>");
        }

        public static string Wrap(this string text, string prefix, string postfix)
        {
            return $"{prefix}{text}{postfix}";
        }
    }
}