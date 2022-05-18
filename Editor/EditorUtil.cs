using System.IO;
using UnityEditor;
using UnityEngine;

namespace Chinchillada
{
    public static class EditorUtil
    {
        public static string GetAssetFolder(Object asset)
        {
            var path = AssetDatabase.GetAssetPath(asset);

            // No path found.
            if (string.IsNullOrEmpty(path))
                return "Assets";

            // Path is already a folder.
            if (!Path.HasExtension(path)) 
                return path;
            
            // Remove the file name from the path.
            var fileName = Path.GetFileName(path);
            return path.Replace(fileName, string.Empty);
        }
    }
}