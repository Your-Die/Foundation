using System;
using System.Collections.Generic;

namespace Chinchillada.Foundation
{
    using System.Linq;

    public static class EnumHelper
    {
        public static IEnumerable<T> GetValues<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            return values.Cast<T>();
        }
        
        public static T Choose<T>(this IRNG random) where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            var index = random.Range(0, values.Length);

            var value = values.GetValue(index);
            return (T) value;
        }
    }
}