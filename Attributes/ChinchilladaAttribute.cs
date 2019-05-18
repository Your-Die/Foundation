using System.Reflection;
using UnityEngine;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Base class for custom attributes that can be applied through <see cref="AttributeHelper.ApplyAttribute{T}"/>.
    /// </summary>
    public abstract class ChinchilladaAttribute : PropertyAttribute
    {
        /// <summary>
        /// Applies the attribute to the <paramref name="field"/> on the <paramref name="behaviour"/>.
        /// </summary>
        /// <param name="behaviour">The behaviour we want to apply this attribute to a field of.</param>
        /// <param name="field">The field we want ot apply this attribute to.</param>
        public abstract void Apply(MonoBehaviour behaviour, FieldInfo field);
    }
}