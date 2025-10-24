using System.Collections;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// Base class for attributes that find component references.
    /// </summary>
    public abstract class ComponentFinderAttribute : PropertyAttribute
    {
        public abstract void Apply(MonoBehaviour behaviour,
                                   object obj,
                                   FieldInfo field,
                                   SearchStrategy? strategyOverride = null,
                                   [CanBeNull] string tagOverride = null);

        public static void ApplyAttribute<TAttribute>(MonoBehaviour behaviour, object obj = null)
            where TAttribute : ComponentFinderAttribute
        {
            obj ??= behaviour;
            AttributeHelper.ForEachAttributedField<TAttribute>(obj, (field, attribute) =>
            {
                attribute.Apply(behaviour, obj, field);
            });
        }

        public static void ApplyAttribute<TAttribute>(MonoBehaviour behaviour,
                                                      SearchStrategy strategy,
                                                      object obj = null,
                                                      string tag = null) where TAttribute : ComponentFinderAttribute
        {
            obj ??= behaviour;
      
            AttributeHelper.ForEachAttributedField<TAttribute>(obj, (field, attribute) =>
            {
                attribute.Apply(behaviour, obj, field, strategy, tag);
            });
        }
    }
}