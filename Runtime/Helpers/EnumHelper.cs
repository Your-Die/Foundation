using System;
using System.Collections.Generic;

namespace Chinchillada
{
    using System.Linq;

    public static class EnumHelper
    {
        public static IEnumerable<T> GetValues<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            return values.Cast<T>();
        }
    }
}