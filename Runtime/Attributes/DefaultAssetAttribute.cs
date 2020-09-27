using System;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Chinchillada.Foundation
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
        private readonly string assetName;

        private readonly string typeFilter;

        /// <summary>
        /// Constructs a new <see cref="DefaultAssetAttribute"/>.
        /// </summary>
        /// <param name="defaultAssetName">The name of the asset we want to use as a default.</param>
        /// <param name="typeFilter">Filter for types to match for.</param>
        public DefaultAssetAttribute(string defaultAssetName, string typeFilter = null)
        {
            this.assetName = defaultAssetName;
            this.typeFilter = typeFilter;
        }

        /// <summary>
        /// Gets the default asset of the given <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type of asset.</param>
        /// <returns>The default asset.</returns>
        public object GetDefaultAsset(Type type)
        {
#if UNITY_EDITOR
            var typeName = string.IsNullOrEmpty(this.typeFilter)
                ? type.Name
                : this.typeFilter;
            
            // Try to find matching assets in the asset database.
            var searchFilter = $"{this.assetName} t:{typeName}";
            var guids = AssetDatabase.FindAssets(searchFilter);
            if (guids.IsEmpty())
                return null;

            // Load the matched asset.
            var guid = guids.First();
            var path = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAssetAtPath(path, type);
#endif
#pragma warning disable 162
            Debug.LogError("Default Asset is requested outside of editor.");
            return null;
#pragma warning restore 162
        }
    }
}
