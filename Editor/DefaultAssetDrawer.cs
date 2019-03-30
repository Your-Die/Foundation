using Chinchillada.Utilities;
using UnityEditor;
using UnityEngine;

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
        DefaultAssetAttribute defaultAssetAttribute = (DefaultAssetAttribute) attribute;
        Object defaultAsset = (Object)defaultAssetAttribute.GetDefaultAsset(fieldInfo.FieldType);

        // Assign.
        property.objectReferenceValue = defaultAsset;
    }
}
