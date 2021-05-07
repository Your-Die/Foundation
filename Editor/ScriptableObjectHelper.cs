using System.IO;
using UnityEditor;
using UnityEngine;

namespace Chinchillada
{
    public static class ScriptableObjectHelper
    {
        /// <summary>
        //  This makes it easy to create, name and place unique new ScriptableObject asset files.
        /// </summary>
        public static T CreateAsset<T>() where T : ScriptableObject
        {
            var asset = ScriptableObject.CreateInstance<T>();

            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                var fileName = Path.GetFileName(path);
                path = path.Replace(fileName, "");
            }

            var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath($"{path}/New {typeof(T)}.asset");

            AssetDatabase.CreateAsset(asset, assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;

            return asset;
        }
    }
}