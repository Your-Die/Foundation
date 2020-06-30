using System;
using System.Collections.Generic;

namespace Chinchillada.Foundation
{
    public static class EnumHelper
    {
        public static T ChooseRandom<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            var index = UnityEngine.Random.Range(0, values.Length);

            var value = values.GetValue(index);
            return (T) value;
        }

        public static IEnumerable<T> RandomValuesDistinct<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            var array = new object[values.Length];
            values.CopyTo(array, 0);

            var indices = array.RandomIndicesDistinct();

            foreach (var index in indices)
            {
                var value = array[index];
                yield return (T) value;
            }
        }
    }
}