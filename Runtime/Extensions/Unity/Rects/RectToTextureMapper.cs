namespace Chinchillada
{
    using UnityEngine;

    public class RectToTextureMapper : RectMapper
    {
        public RectToTextureMapper(Rect rect, Texture2D texture) : base(rect, texture.GetRect())
        {
        }
    }
}