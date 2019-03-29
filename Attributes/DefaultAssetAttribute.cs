using System;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Chinchillada.Utilities
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
        private readonly string _assetName;

        /// <summary>
        /// Constructs a new <see cref="DefaultAssetAttribute"/>.
        /// </summary>
        /// <param name="defaultAssetName">The name of the asset we want to use as a default.</param>
        public DefaultAssetAttribute(string defaultAssetName)
        {
            _assetName = defaultAssetName;
        }

        /// <summary>
        /// Gets the default asset of the given <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type of asset.</param>
        /// <returns>The default asset.</returns>
        public object GetDefaultAsset(Type type)
        {
#if UNITY_EDITOR
            // Try to find matching assets in the asset database.
            var searchFilter = $"{_assetName} t:{type.Name}";
            var guids = AssetDatabase.FindAssets(searchFilter);
            if (guids.IsEmpty())
                return null;

            // Load the matched asset.
            string guid = guids.First();
            string path = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAssetAtPath(path, type);
#endif
            Debug.LogError("Default Asset is requested outside of editor.");
            return null;
        }
    }
}
