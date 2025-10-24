using System;
using System.Linq;
using System.Reflection;
using Sirenix.Utilities;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// Static class containing helper functions for use with attributes.
    /// </summary>
    public static class AttributeHelper
    {
        public static void ForEachAttributedField<TAttribute>(object obj, Action<FieldInfo, TAttribute> action)
            where TAttribute : PropertyAttribute
        {
            if (obj == null)
                return;

            Type type = obj.GetType();
            
            ForEachField(type, field =>
            {
                Attribute attribute = field.GetCustomAttributes(typeof(TAttribute)).FirstOrDefault();
                if (attribute == null)
                    return;
                
                var typedAttribute = (TAttribute)attribute;
                action(field, typedAttribute);
            });
        }

        public static void ForEachField(Type type, Action<FieldInfo> action)
        {
            const BindingFlags bindingFlags = BindingFlags.Instance |
                                              BindingFlags.DeclaredOnly |
                                              BindingFlags.NonPublic |
                                              BindingFlags.Public;

            ForEachBaseClass(type, baseClass =>
            {
                var fields = baseClass.GetFields(bindingFlags);
                foreach (FieldInfo field in fields)
                    action(field);
            });
        }

        public static void ForEachBaseClass(Type type, Action<Type> action, bool includeSelf = false)
        {
            if (includeSelf)
                action(type);

            for (Type current = type.BaseType; current != null; current = current.BaseType)
                action(current);
        }
    }
}