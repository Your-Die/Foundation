using System;
using UnityEngine;

namespace Chinchillada
{
    public static class TextureExtensions
    {
        public static Texture2D Copy(this Texture2D texture)
        {
            var output = new Texture2D(texture.width, texture.height);
            
            var pixels = texture.GetPixels();

            output.SetPixels(pixels);
            output.Apply();

            return output;
        }

        /// <remarks>
        /// Taken from http://wiki.unity3d.com/index.php/TextureDrawLine
        /// </remarks>
        public static void DrawLine(this Texture2D texture, Vector2Int from, Vector2Int to, Color color)
        {
            var dy = to.y - from.y;
            var dx = to.x - from.x;

            int stepX, stepY;

            if (dy < 0)
            {
                dy = -dy;
                stepY = -1;
            }
            else
            {
                stepY = 1;
            }

            if (dx < 0)
            {
                dx = -dx;
                stepX = -1;
            }
            else
            {
                stepX = 1;
            }

            dy <<= 1;
            dx <<= 1;

            float fraction;

            texture.SetPixel(from.x, from.y, color);
            if (dx > dy)
            {
                fraction = dy - (dx >> 1);
                while (Mathf.Abs(from.x - to.x) > 1)
                {
                    if (fraction >= 0)
                    {
                        from.y += stepY;
                        fraction -= dx;
                    }

                    from.x += stepX;
                    fraction += dy;
                    texture.SetPixel(from.x, from.y, color);
                }
            }
            else
            {
                fraction = dx - (dy >> 1);
                while (Mathf.Abs(from.y - to.y) > 1)
                {
                    if (fraction >= 0)
                    {
                        from.x += stepX;
                        fraction -= dy;
                    }

                    from.y += stepY;
                    fraction += dx;
                    texture.SetPixel(from.x, from.y, color);
                }
            }
        }
    }
}