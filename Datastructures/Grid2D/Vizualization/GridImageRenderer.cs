using UnityEngine;
using UnityEngine.UI;

namespace Chinchillada.Grid.Visualization
{
    public class GridImageRenderer : GridTextureRendererBase
    {
        [SerializeField] private RawImage image;
        
        protected override void SetTexture(Texture texture)
        {
            this.image.texture = texture;
        }
    }
}