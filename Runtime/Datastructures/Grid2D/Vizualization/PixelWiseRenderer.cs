using System.Collections.Generic;
using Chinchillada.Colors;
using Chinchillada.Foundation;
using UnityEngine;

namespace Chinchillada.Grid.Visualization
{
    public class PixelWiseRenderer : GridRendererBase
    {
        [SerializeField] private float spacing;

        [SerializeField, FindComponent] private Transform topLeft;

        [SerializeField] private SpriteRenderer pixelPrefab;

        [SerializeField] private IColorScheme colorScheme;

        private SpriteRenderer[,] pixels;

        private readonly LinkedList<SpriteRenderer> inactivePixels = new LinkedList<SpriteRenderer>();

        protected override void RenderGrid(Grid2D grid)
        {
            this.PreparePixels(grid.Width, grid.Height);

            for (var x = 0; x < grid.Width; x++)
            for (var y = 0; y < grid.Height; y++)
            {
                var value = grid[x, y];
                var color = this.colorScheme[value];

                this.pixels[x, y].color = color;
            }
        }


        private void PreparePixels(int width, int height)
        {
            this.DisablePixels();
            this.EnsurePixels(width * height);

            this.pixels = new SpriteRenderer[width, height];
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                var pixel = this.inactivePixels.GrabFirst();
                pixel.enabled = true;
                
                this.pixels[x, y] = pixel;

                var offset = new Vector3(x, y) * this.spacing;
                pixel.transform.position = this.topLeft.position + offset;
            }
        }

        private void EnsurePixels(int amount)
        {
            var pixelsCount = this.inactivePixels.Count;
            if (amount <= pixelsCount)
                return;

            var difference = amount - pixelsCount;

            for (var i = 0; i < difference; i++)
            {
                var pixel = Instantiate(this.pixelPrefab, this.transform);
                this.inactivePixels.AddLast(pixel);
            }
        }

        private void DisablePixels()
        {
            if (this.pixels == null)
                return;

            var width = this.pixels.GetLength(0);
            var height = this.pixels.GetLength(1);

            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                var pixel = this.pixels[x, y];
                pixel.enabled = false;

                this.inactivePixels.AddFirst(pixel);
            }
        }
    }
}