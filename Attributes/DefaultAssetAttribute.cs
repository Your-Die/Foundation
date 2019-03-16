using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Chinchillada.Utilities
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DefaultAssetAttribute : ChinchilladaAttribute
    {
        private readonly string _searchFilter;

        public DefaultAssetAttribute(string searchFilter)
        {
            _searchFilter = searchFilter;
        }

        public override void Apply(MonoBehaviour behaviour, FieldInfo field)
        {
            object asset = GetDefaultAsset(field);
            field.SetValue(behaviour, asset);
        }

        private object GetDefaultAsset(FieldInfo field)
        {
            var guids = AssetDatabase.FindAssets(_searchFilter).ToArray();
            if (guids.IsEmpty())
                return null;

            string guid = guids.First();
            string path = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAssetAtPath(path, field.FieldType);
        }

    }
}
