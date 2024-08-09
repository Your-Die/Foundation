using System.Collections;
using System.Reflection;
using UnityEngine;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// Base class for attributes that find component references.
    /// </summary>
    public abstract class ComponentFinderAttribute : PropertyAttribute
    {
        public abstract void Apply(MonoBehaviour behaviour, object obj, FieldInfo field);

        public abstract void Apply(MonoBehaviour behaviour,
                                   object obj,
                                   FieldInfo field,
                                   SearchStrategy searchStrategy,
                                   string tag);

        public static void ApplyAttribute<TAttribute>(MonoBehaviour behaviour, object obj = null)
            where TAttribute : ComponentFinderAttribute
        {
            obj ??= behaviour;
            var attributedFields = AttributeHelper.GetAttributedFields<TAttribute>(obj);

            foreach (var (field, attribute) in attributedFields)
                attribute.Apply(behaviour, obj, field);
        }

        public static void ApplyAttribute<TAttribute>(MonoBehaviour behaviour,
                                                      SearchStrategy strategy,
                                                      object obj = null,
                                                      string tag = null) where TAttribute : ComponentFinderAttribute
        {
            obj ??= behaviour;
            var attributedFields = AttributeHelper.GetAttributedFields<TAttribute>(obj);

            foreach (var (field, attribute) in attributedFields)
                attribute.Apply(behaviour, obj, field, strategy, tag);
        }
    }
}