using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// Static class containing helper functions for use with attributes.
    /// </summary>
    public static class AttributeHelper
    {
        public static IEnumerable<(FieldInfo field, TAttribute)> GetAttributedFields<TAttribute>(object obj)
            where TAttribute : PropertyAttribute
        {
            var fields = GetAllFields(obj);
            var attributeType = typeof(TAttribute);

            foreach (var field in fields)
            {
                var attributes = field.GetCustomAttributes(attributeType).ToList();
                if (attributes.IsEmpty())
                    continue;

                var attribute = (TAttribute)attributes.First();
                yield return (field, attribute);
            }
        }

        private static IEnumerable<FieldInfo> GetAllFields(object obj)
        {
            const BindingFlags bindingFlags = BindingFlags.Instance |
                                              BindingFlags.NonPublic |
                                              BindingFlags.Public;
            var type = obj.GetType();
            return type.GetFields(bindingFlags);
        }
    }
}
