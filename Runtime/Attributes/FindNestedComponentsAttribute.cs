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
            var nestedFields = AttributeHelper.GetAttributedFields<ComponentFinderAttribute>(nestedObject);
            
            foreach (var (nestedField, attribute) in nestedFields)
                attribute.Apply(behaviour, nestedObject, nestedField, strategy, tag);
        }

        private static void ResolveNestedCollection(MonoBehaviour behaviour,
                                                    IEnumerable collection,
                                                    SearchStrategy? strategy,
                                                    string tag)
        {
            foreach (var nestedObject in collection)
                ResolveNestedObject(behaviour, nestedObject, strategy, tag);
        }
    }
}