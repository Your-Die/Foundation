using System;
using System.Collections;

namespace Chinchillada.Foundation
{
    public static class TypeExtensions
    {
        public static bool IsList(this Type type)
        {
            var memberInfo = type.GetInterface(nameof(IList));
            return memberInfo != null;
        }
    }
}    