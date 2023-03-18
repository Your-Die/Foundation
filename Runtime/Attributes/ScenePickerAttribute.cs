using UnityEngine;

namespace Chinchillada
{
    public class ScenePickerAttribute : PropertyAttribute
        {
            public static string LoadableName(string path)
            {
                string start = "Assets/";
                string end = ".unity";

                if (path.EndsWith(end))
                {
                    path = path.Substring(0, path.LastIndexOf(end));
                }

                if (start.StartsWith(start))
                {
                    path = path.Substring(start.Length);
                }

                return path;
            }
        
    }
}