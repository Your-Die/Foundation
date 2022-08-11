using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Chinchillada
{
    /// <summary>
    /// Static class containing helper functions for use with attributes.
    /// </summary>
    public static class AttributeHelper
    {
        public static IEnumerable<(FieldInfo field, TAttribute)> GetAttributedFields<TAttribute>(object obj)
            where TAttribute : PropertyAttribute
        {
            var type   = obj.GetType();
            var fields = GetAllFields(type);

            var attributeType = typeof(TAttribute);

            foreach (var field in fields)
            {
                var attributes = field.GetCustomAttributes(attributeType).ToList();
                if (attributes.IsEmpty())
                    continue;

                var attribute = (TAttribute) attributes.First();
                yield return (field, attribute);
            }
        }


        private static IEnumerable<FieldInfo> GetAllFields(Type type)
        {
            const BindingFlags bindingFlags = BindingFlags.Instance     |
                                              BindingFlags.DeclaredOnly |
                                              BindingFlags.NonPublic    |
                                              BindingFlags.Public;

            var types = GetBaseClasses(type);
            return types.SelectMany(baseType => baseType.GetFields(bindingFlags));
        }

        private static IEnumerable<Type> GetBaseClasses(Type type)
        {
            for (var current = type; current != null; current = current.BaseType)
                yield return current;
        }
    }
}