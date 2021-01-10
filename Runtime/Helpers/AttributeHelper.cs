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
            var type = obj.GetType();
            var fields = GetAllFields(type);

            foreach (var field in fields)
            {
                var value = field.GetValue(obj);
                if (!IsNull(value))
                    continue;

                var attributes = field.GetCustomAttributes(typeof(TAttribute)).ToList();
                if (attributes.IsEmpty())
                    continue;

                var attribute = (TAttribute) attributes.First();
                yield return(field, attribute);
            }
        }

        public static IEnumerable<FieldInfo> GetAllFields(Type type)
        {
            const BindingFlags bindingFlags = BindingFlags.Instance     |
                                              BindingFlags.DeclaredOnly |
                                              BindingFlags.NonPublic    |
                                              BindingFlags.Public;

            var types = GetBaseClasses(type, true);
            return types.SelectMany(baseType => baseType.GetFields(bindingFlags));
        }

        public static IEnumerable<Type> GetBaseClasses(Type type, bool includeSelf = false)
        {
            if (includeSelf)
                yield return type;

            for (var current = type.BaseType; current != null; current = current.BaseType)
                yield return current;
        }

        private static bool IsNull(object item)
        {
            switch (item)
            {
                case null: return true;
                case Object unityObject: return unityObject == null;
                default: return false;
            }
        }
    }
}
