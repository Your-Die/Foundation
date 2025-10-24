using System.Collections;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Chinchillada.Foundation
{
    public class FindNestedComponentsAttribute : ComponentFinderAttribute
    {
        public override void Apply(MonoBehaviour behaviour,
                                   object obj,
                                   FieldInfo field,
                                   SearchStrategy? strategyOverride = null,
                                   string tagOverride = null)
        {
            if (typeof(Object).IsAssignableFrom(field.FieldType))
                return;

            var nestedObject = field.GetValue(obj);

            if (nestedObject is IEnumerable collection)
                ResolveNestedCollection(behaviour, collection, strategyOverride, tagOverride);
            else
                ResolveNestedObject(behaviour, nestedObject, strategyOverride, tagOverride);
        }


        private static void ResolveNestedObject(MonoBehaviour behaviour,
                                                object nestedObject,
                                                SearchStrategy? strategy,
                                                string tag)
        {
            var action = new NestedAttributeAction<ComponentFinderAttribute>(behaviour, nestedObject, strategy, tag);
            AttributeHelper.ForEachAttributedField<NestedAttributeAction<ComponentFinderAttribute>, ComponentFinderAttribute>(nestedObject, ref action);
        }

        private static void ResolveNestedCollection(MonoBehaviour behaviour,
                                                    IEnumerable collection,
                                                    SearchStrategy? strategy,
                                                    string tag)
        {
            foreach (var nestedObject in collection) 
                ResolveNestedObject(behaviour, nestedObject, strategy, tag);
        }

        private readonly struct NestedAttributeAction<TAttribute> : IPerformantAction<FieldInfo, TAttribute>
            where TAttribute : ComponentFinderAttribute
        {
            private readonly MonoBehaviour  behaviour;
            private readonly object         nestedObject;
            private readonly SearchStrategy? strategy;
            private readonly string         tag;

            public NestedAttributeAction(MonoBehaviour behaviour,
                                         object nestedObject,
                                         SearchStrategy? strategy,
                                         string tag)
            {
                this.behaviour = behaviour;
                this.nestedObject = nestedObject;
                this.strategy = strategy;
                this.tag = tag;
            }

            public void Invoke(FieldInfo field, TAttribute attribute)
            {
                attribute.Apply(this.behaviour, this.nestedObject, field, this.strategy, this.tag);
            }
        }
    }
}