using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Chinchillada.Utilities
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DefaultAssetAttribute : PropertyAttribute
    {
        private readonly string _searchFilter;

        public DefaultAssetAttribute(string searchFilter)
        {
            _searchFilter = searchFilter;
        }

        public object GetDefaultAsset(Type type)
        {
            var guids = AssetDatabase.FindAssets(_searchFilter);
            if (guids.IsEmpty())
                return null;

            string guid = guids.First();
            string path = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAssetAtPath(path, type);
        }
    }
}
