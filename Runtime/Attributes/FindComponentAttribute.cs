using System.Collections;
using System.Linq;
using UnityEngine;
using System.Reflection;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// Attribute meant for removing the boilerplate code that is often found in Unity Monobehaviour classes.
    /// This attribute automates the setting up of references to other components, instead of having to manually write the <see cref="GetComponent"/>
    /// for each component reference that is necessary.
    /// </summary>
    public class FindComponentAttribute : ComponentFinderAttribute
    {
        /// <summary>
        /// The search <see cref="SearchStrategy"/> that we want to use when looking for matching components.
        /// </summary>
        private readonly SearchStrategy strategy;

        private readonly string tag;

        /// <summary>
        /// Constructs a new <see cref="FindComponentAttribute"/>.
        /// </summary>
        /// <param name="strategy">
        /// The search <see cref="SearchStrategy"/> that we want to use when looking for matching components.
        /// </param>
        /// <param name="tag">
        /// If provided, the search will only accept components on gameObjects with the matching tag.
        /// </param>
        public FindComponentAttribute(SearchStrategy strategy = SearchStrategy.FindComponent, string tag = null)
        {
            this.strategy = strategy;
            this.tag = tag;
        }

        public override void Apply(MonoBehaviour behaviour,
                                   object obj,
                                   FieldInfo field,
                                   SearchStrategy? searchStrategy = null,
                                   string searchTag = null)
        {
            searchStrategy ??= this.strategy;
            searchTag ??= this.tag;
            
            var value = field.GetValue(obj);

            if (value is IList)
                ResolveCollection(behaviour, obj, field, searchStrategy.Value, searchTag);
            else
                ResolveField(behaviour, obj, field, searchStrategy.Value, searchTag);
        }

        private static void ResolveField(Component behaviour, object obj, FieldInfo field, SearchStrategy strategy, string tag)
        {
            var fieldValue = field.GetValue(obj);
            if (!Equality.UnityNull(fieldValue))
                return;

            var result = tag != null
                ? strategy.FindComponent(behaviour.gameObject, field.FieldType, tag)
                : strategy.FindComponent(behaviour.gameObject, field.FieldType);
                
            field.SetValue(obj, result);
        }

        private static void ResolveCollection(Component behaviour,
                                              object obj,
                                              FieldInfo field,
                                              SearchStrategy strategy,
                                              string searchTag)
        {
            if (field.FieldType.IsGenericType == false)
            {
                var message = $"{typeof(FindComponentAttribute)} found on non-generic collection field: {field}. " +
                              "This is not currently supported.";
                Debug.LogWarning(message);
                return;
            }

            var itemTypes = field.FieldType.GetGenericArguments();
            if (itemTypes.Length > 1)
            {
                var message = $"{typeof(FindComponentAttribute)} found on collection field with multiple type arguments: {field}  " +
                              "This is not currently supported.";
                Debug.LogWarning(message);
                return;
            }

            var itemType = itemTypes.First();

            var fieldValue = field.GetValue(obj);
            if (fieldValue == null)
                return;

            var list = (IList) fieldValue;

            var items = searchTag != null
                ? strategy.FindComponents(behaviour.gameObject, itemType, searchTag)
                : strategy.FindComponents(behaviour.gameObject, itemType);
                
            var newItems = items.Except(IsInvalid);

            foreach (var item in newItems.ToArray())
                list.Add(item);

            bool IsInvalid(Component component) => component == behaviour || list.Contains(component);
        }
    }
}