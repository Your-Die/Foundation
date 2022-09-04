using UnityEngine;

namespace Chinchillada
{
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

        public static RectInt GetRect(this Texture2D texture)
        {
            return new RectInt(0, 0, texture.width, texture.height);
        }

        public static void DrawSquare(this Texture2D texture, RectInt rect, Color color)
        {
            var cells = rect.EnumerateCells();

            foreach (Vector2Int cell in cells) 
                texture.SetPixel(cell.x, cell.y, color);
        }
    }
}