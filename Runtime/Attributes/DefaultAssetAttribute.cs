using System;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Chinchillada
{
    /// <summary>
    /// This attribute is meant for serialized fields of Unity asset types, initially developed for <see cref="ScriptableObject"/>.
    /// The attribute takes a string that is used to find an asset in the asset database that is used in the Unity editor if no other
    /// instance of the asset has been assigned to the field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DefaultAssetAttribute : PropertyAttribute
    {
        /// <summary>
        /// The name of the asset we want to use as a default.
        /// </summary>
        public string AssetName { get; }

        public          string TypeFilter { get; }

        /// <summary>
        /// Constructs a new <see cref="DefaultAssetAttribute"/>.
        /// </summary>
        /// <param name="defaultAssetName">The name of the asset we want to use as a default.</param>
        /// <param name="typeFilter">Filter for types to match for.</param>
        public DefaultAssetAttribute(string defaultAssetName, string typeFilter = null)
        {
            this.AssetName = defaultAssetName;
            this.TypeFilter = typeFilter;
        }
    }
}
