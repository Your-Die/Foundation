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

            var action = new ApplyAttributeAction<TAttribute>(behaviour, obj);
            AttributeHelper.ForEachAttributedField<ApplyAttributeAction<TAttribute>, TAttribute>(obj, ref action);
        }

        public static void ApplyAttribute<TAttribute>(MonoBehaviour behaviour,
                                                      SearchStrategy strategy,
                                                      object obj = null,
                                                      string tag = null) where TAttribute : ComponentFinderAttribute
        {
            obj ??= behaviour;

            var action = new ApplyStrategyAttributeAction<TAttribute>(behaviour, obj, strategy, tag);
            AttributeHelper.ForEachAttributedField<ApplyStrategyAttributeAction<TAttribute>, TAttribute>(obj, ref action);
        }
    }

    public readonly struct ApplyAttributeAction<TAttribute> : IPerformantAction<FieldInfo, TAttribute>
        where TAttribute : ComponentFinderAttribute
    {
        private readonly MonoBehaviour behaviour;
        private readonly object        obj;

        public ApplyAttributeAction(MonoBehaviour behaviour, object obj)
        {
            this.behaviour = behaviour;
            this.obj = obj;
        }

        public void Invoke(FieldInfo field, TAttribute attribute)
        {
            attribute.Apply(this.behaviour, this.obj, field);
        }
    }
    
    public readonly struct ApplyStrategyAttributeAction<TAttribute> : IPerformantAction<FieldInfo, TAttribute>
        where TAttribute : ComponentFinderAttribute
    {
        private readonly MonoBehaviour  behaviour;
        private readonly object         obj;
        private readonly SearchStrategy? strategy;
        private readonly string         tag;

        public ApplyStrategyAttributeAction(MonoBehaviour behaviour, object obj, SearchStrategy? strategy, string tag)
        {
            this.behaviour = behaviour;
            this.obj = obj;
            this.strategy = strategy;
            this.tag = tag;
        }

        public void Invoke(FieldInfo field, TAttribute attribute)
        {
            attribute.Apply(this.behaviour, this.obj, field, this.strategy, this.tag);
        }
    }
}