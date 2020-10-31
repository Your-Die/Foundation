using System;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Chinchillada.Foundation
{
    public class FindNestedComponentsAttribute : ComponentFinderAttribute
    {
        public override void Apply(MonoBehaviour behaviour, object obj, FieldInfo field)
        {
            if (typeof(Object).IsAssignableFrom(field.FieldType))
            {
                var message = $"{nameof(FindNestedComponentsAttribute)} found on a type that derives from {nameof(Object)}: " +
                              $"{Environment.NewLine}" +
                              $"{nameof(MonoBehaviour)}: {behaviour} - Field: {field}";
               
                Debug.LogWarning(message);
                return;
            }

            var nestedObject = field.GetValue(obj);
            var nestedFields = AttributeHelper.GetAttributedFields<ComponentFinderAttribute>(nestedObject);
            
            foreach (var (nestedField, attribute) in nestedFields)
                attribute.Apply(behaviour, nestedObject, nestedField);
        }

        public override void Apply(MonoBehaviour behaviour, object obj, FieldInfo field, SearchStrategy strategy)
        {
            var nestedObject = field.GetValue(obj);
            var nestedFields = AttributeHelper.GetAttributedFields<ComponentFinderAttribute>(nestedObject);
            
            foreach (var (nestedField, attribute) in nestedFields)
                attribute.Apply(behaviour, nestedObject, nestedField, strategy);
        }
    }
}