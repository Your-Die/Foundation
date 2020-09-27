using Chinchillada.Foundation;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Grid.Visualization
{
    public class Grayscaler : ChinchilladaBehaviour
    {
        [SerializeField] private Renderer textureRenderer;

        private Texture Texture
        {
            get => this.textureRenderer.material.mainTexture;
            set => this.textureRenderer.material.mainTexture = value;
        }

        [Button]
        public void Apply()
        {
            var texture = (Texture2D) this.Texture;

            var pixels = texture.GetPixels();
            for (var i = 0; i < pixels.Length; i++)
            {
                var pixel = pixels[i];
                var grayscale = pixel.grayscale;

                var grayscaleColor = new Color(grayscale, grayscale, grayscale, pixel.a);
                pixels[i] = grayscaleColor;
            }

            texture.SetPixels(pixels);
            texture.Apply();

            this.Texture = texture;
        }
    }
}