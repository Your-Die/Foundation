using System;
using System.Linq;
using Chinchillada;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// <see cref="PropertyDrawer"/> for <see cref="DefaultAssetAttribute"/>.
/// The drawer tries to find and assign the default asset described in the attribute.
/// </summary>
[CustomPropertyDrawer(typeof(DefaultAssetAttribute))]
public class DefaultAssetDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Draw the property gui.
        EditorGUILayout.PropertyField(property, label, true);

        // We only apply the default if there is no assigned asset.
        if (property.objectReferenceValue != null)
            return;

        // Get the default asset.
        property.objectReferenceValue = this.GetDefaultAsset(this.fieldInfo.FieldType);;
    }
    
    /// <summary>
    /// Gets the default asset of the given <paramref name="type"/>.
    /// </summary>
    /// <param name="type">The type of asset.</param>
    /// <returns>The default asset.</returns>
    private Object GetDefaultAsset(Type type)
    {
        var defaultAssetAttribute = (DefaultAssetAttribute)this.attribute;
        
        var typeName = string.IsNullOrEmpty(defaultAssetAttribute.TypeFilter)
            ? type.Name
            : defaultAssetAttribute.TypeFilter;
            
        // Try to find matching assets in the asset database.
        var searchFilter = $"{defaultAssetAttribute.AssetName} t:{typeName}";
        var guids = AssetDatabase.FindAssets(searchFilter);
        if (guids.IsEmpty())
            return null;

        // Load the matched asset.
        var guid = guids.First();
        var path = AssetDatabase.GUIDToAssetPath(guid);
        return AssetDatabase.LoadAssetAtPath(path, type);
    }
}
