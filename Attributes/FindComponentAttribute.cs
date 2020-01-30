using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using Object = UnityEngine.Object;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Enum containing possible search strategies.
    /// </summary>
    public enum SearchStrategy
    {
        FindComponent,
        InParent,
        InChildren,
        OnlyChildren,
        Anywhere
    }

    /// <summary>
    /// Attribute meant for removing the boilerplate code that is often found in Unity Monobehaviour classes.
    /// This attribute is meant to automate the setting up of references to other components, instead of having to manually write the <see cref="GetComponent"/>
    /// for each component reference that is necessary.
    /// </summary>
    public class FindComponentAttribute : ChinchilladaAttribute
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
        public override void Apply(MonoBehaviour behaviour, FieldInfo field)
        {
            Type type = field.FieldType;
            Component component = this.FindComponent(behaviour, type);

            field.SetValue(behaviour, component);
        }

        /// <summary>
        /// Tries to find a matching <see cref="Component"/> with the current <see cref="SearchStrategy"/>.
        /// </summary>
        /// <param name="behaviour">The behaviour we want to find a component of.</param>
        /// <param name="type">The type of component we are looking for.</param>
        /// <returns>The found component, or null.</returns>
        private Component FindComponent(MonoBehaviour behaviour, Type type)
        {
            switch (this.strategy)
            {
                case SearchStrategy.FindComponent:
                    return behaviour.GetComponent(type);
                case SearchStrategy.InParent:
                    return behaviour.GetComponentInParent(type);
                case SearchStrategy.InChildren:
                    return behaviour.GetComponentInChildren(type);
                case SearchStrategy.OnlyChildren:
                    return behaviour.GetComponentsInDirectChildren(type).First();
                case SearchStrategy.Anywhere:
                    return (Component) Object.FindObjectOfType(type);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
