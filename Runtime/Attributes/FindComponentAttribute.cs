using UnityEngine;
using System.Linq;
using System.Reflection;
using Object = UnityEngine.Object;

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

        /// <summary>
        /// Constructs a new <see cref="FindComponentAttribute"/>.
        /// </summary>
        /// <param name="strategy">The search <see cref="SearchStrategy"/> that we want to use when looking for matching components.</param>
        public FindComponentAttribute(SearchStrategy strategy = SearchStrategy.FindComponent)
        {
            this.strategy = strategy;
        }

        /// <inheritdoc />
        public override void Apply(MonoBehaviour behaviour, object obj,  FieldInfo field)
        {
            this.Apply(behaviour, obj, field, this.strategy);
        }

        public override void Apply(MonoBehaviour behaviour, object obj, FieldInfo field, SearchStrategy searchStrategy)
        {
            if (field.GetValue(obj) != null)
                return;
            
            var type = field.FieldType;
            var component = searchStrategy.FindComponent(behaviour.gameObject, type);

            field.SetValue(obj, component);
        }
    }
}
