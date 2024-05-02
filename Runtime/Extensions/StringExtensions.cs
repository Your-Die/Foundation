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

        public static string JoinWithNewLine<T>(this IEnumerable<T> items)
        {
            return items.JoinWith(Environment.NewLine);
        }

        public static string JoinWithComma<T>(this IEnumerable<T> items)
        {
            return items.JoinWith(", ");
        }

        public static string JoinWith<T>(this IEnumerable<T> items, string separator) => string.Join(separator, items);
    }
}