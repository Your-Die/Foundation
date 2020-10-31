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
                return;

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