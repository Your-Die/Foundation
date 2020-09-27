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
        public static void ApplyAttribute<TAttribute>(MonoBehaviour behaviour) where TAttribute : ChinchilladaAttribute
        {
            var attributedFields = GetAttributedFields<TAttribute>(behaviour);

            foreach ((FieldInfo field, TAttribute attribute) in attributedFields)
                attribute.Apply(behaviour, field);
        }

        public static IEnumerable<(FieldInfo field, TAttribute)> GetAttributedFields<TAttribute>(Object obj)
            where TAttribute : PropertyAttribute
        {
            const BindingFlags bindingFlags = BindingFlags.Instance |
                                              BindingFlags.NonPublic |
                                              BindingFlags.Public;
            Type type = obj.GetType();
            var fields = type.GetFields(bindingFlags);

            foreach (FieldInfo field in fields)
            {
                if (field.GetValue(obj) != null)
                    continue;

                var attributes = field.GetCustomAttributes(typeof(TAttribute)).ToList();
                if (attributes.IsEmpty())
                    continue;

                TAttribute attribute = (TAttribute)attributes.First();
                yield return (field, attribute);
            }
        }
    }
}
