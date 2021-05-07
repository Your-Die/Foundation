namespace Chinchillada
{
    using System.Collections.Generic;

    public static class CharExtensions
    {
        public static string BuildString(this IEnumerable<char> characters)
        {
            return string.Join(string.Empty, characters);
        }
    }
}