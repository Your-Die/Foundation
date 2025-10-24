using System;
using System.Linq;
using System.Reflection;
using Sirenix.Utilities;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Chinchillada.Foundation
{
    public interface IPerformantAction<in TFirst, in TSecond>
    {
        void Invoke(TFirst first, TSecond second);
    }

    /// <summary>
    /// Static class containing helper functions for use with attributes.
    /// </summary>
    public static class AttributeHelper
    {
        public static void ForEachAttributedField<TAction, TAttribute>(object obj, ref TAction action)
            where TAction : struct, IPerformantAction<FieldInfo, TAttribute>
            where TAttribute : PropertyAttribute
        {
            if (obj == null)
                return;

            Type type = obj.GetType();

            const BindingFlags bindingFlags = BindingFlags.Instance |
                                              BindingFlags.DeclaredOnly |
                                              BindingFlags.NonPublic |
                                              BindingFlags.Public;


            for (Type current = type; current != null; current = current.BaseType)
            {
                var fields = current.GetFields(bindingFlags);
                foreach (FieldInfo field in fields)
                {
                    Attribute attribute = field.GetCustomAttributes(typeof(TAttribute)).FirstOrDefault();
                    if (attribute == null)
                        continue;

                    var typedAttribute = (TAttribute)attribute;
                    action.Invoke(field, typedAttribute);
                }
            }
        }
    }
}