using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Chinchillada.Utilities
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DefaultAssetAttribute : PropertyAttribute
    {
        private readonly string _assetName;

        public DefaultAssetAttribute(string defaultAssetName)
        {
            _assetName = defaultAssetName;
        }

        public object GetDefaultAsset(Type type)
        {
            var searchFilter = $"{_assetName} t:{type.Name}";
            var guids = AssetDatabase.FindAssets(searchFilter);
            if (guids.IsEmpty())
                return null;

            string guid = guids.First();
            string path = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAssetAtPath(path, type);
        }
    }
}
