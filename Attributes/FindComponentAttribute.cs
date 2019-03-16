using UnityEngine;
using System;
using System.Reflection;

namespace Chinchillada.Utilities
{
    public enum SearchStrategy
    {
        FindComponent,
        InParent,
        InChildren,
    }

    public class FindComponentAttribute : ChinchilladaAttribute
    {
        private readonly SearchStrategy _strategy;

        public bool Multiple { get; }

        public FindComponentAttribute(SearchStrategy strategy = SearchStrategy.FindComponent, bool multiple = false)
        {
            _strategy = strategy;
            Multiple = multiple;
        }

        public override void Apply(MonoBehaviour behaviour, FieldInfo field)
        {
            Type type = field.FieldType;
            Component component = FindComponent(behaviour, type);

            field.SetValue(behaviour, component);
        }

        private Component FindComponent(MonoBehaviour behaviour, Type type)
        {
            switch (_strategy)
            {
                case SearchStrategy.FindComponent:
                    return behaviour.GetComponent(type);
                case SearchStrategy.InParent:
                    return behaviour.GetComponentInParent(type);
                case SearchStrategy.InChildren:
                    return behaviour.GetComponentInChildren(type);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
