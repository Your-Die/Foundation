using UnityEngine;

namespace Chinchillada
{
    using System.Collections.Generic;

    public static class TextureExtensions
    {
        public static Texture2D Copy(this Texture2D texture)
        {
            var output = new Texture2D(texture.width, texture.height);

            for (var x = 0; x < texture.width; x++)
            for (var y = 0; y < texture.height; y++)
            {
                var pixel = texture.GetPixel(x, y);
                output.SetPixel(x, y, pixel);
            }

            output.Apply();

            return output;
        }

        public static Texture2D Upscale(this Texture2D input, int upscale)
        {
            var width  = input.width  * upscale;
            var height = input.height * upscale;

            var upscaledTexture = new Texture2D(width, height);

            for (var xInput = 0; xInput < input.width; xInput++)
            for (var yInput = 0; yInput < input.height; yInput++)
            {
                var pixel = input.GetPixel(xInput, yInput);
                
                for (var xOffset = 0; xOffset < upscale; xOffset++)
                for (var yOffset = 0; yOffset < upscale; yOffset++)
                {
                    var x = xInput * upscale + xOffset;
                    var y = yInput * upscale + yOffset;
                    
                    upscaledTexture.SetPixel(x, y, pixel);
                }
            }

            upscaledTexture.Apply();
            return upscaledTexture;
        }
        
        
        /// <remarks>
        /// Taken from http://wiki.unity3d.com/index.php/TextureDrawLine
        /// </remarks>
        public static void DrawLine(this Texture2D texture, Vector2Int from, Vector2Int to, Color color)
        {
            var points = Lines.GetPointsOnLine(from, to);
            
            foreach (var point in points) 
                texture.SetPixel(point.x, point.y, color);
        }


        public static RectInt GetRect(this Texture2D texture)
        {
            return new RectInt(0, 0, texture.width, texture.height);
        }

        public static void DrawRect(this Texture2D texture, RectInt rect, Color color)
        {
            foreach (Vector2Int cell in rect.allPositionsWithin) 
                texture.SetPixel(cell.x, cell.y, color);
        }
    }
}