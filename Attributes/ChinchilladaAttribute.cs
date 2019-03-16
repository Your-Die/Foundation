using System.Reflection;
using UnityEngine;

namespace Chinchillada.Utilities
{
    public abstract class ChinchilladaAttribute : PropertyAttribute
    {
        public abstract void Apply(MonoBehaviour behaviour, FieldInfo field);
    }
}