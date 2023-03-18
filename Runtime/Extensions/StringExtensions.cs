using System;
using System.Collections.Generic;

namespace Chinchillada
{
    public static class StringExtensions
    {
        public static string Decapitalize(this string text)
        {
            if (text.Length == 0)
                return text;

            var firstLetter = text[0];
            if (char.IsLower(firstLetter))
                return text;

            var lower = char.ToLower(firstLetter);
            return lower + text.Substring(1);
        }

        public static string JoinWithNewLine(this IEnumerable<object> items)
        {
            return string.Join(Environment.NewLine, items);
        }
    }
}