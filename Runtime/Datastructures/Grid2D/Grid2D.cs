using System;
using System.Collections;
using System.Collections.Generic;
using Chinchillada.Colors;
using UnityEngine;

namespace Chinchillada.Grid
{
    [Serializable]
    public class Grid2D : IEnumerable<int>, IGrid2D<int>
    {
        [SerializeField] private int[,] items;

        [SerializeField] private int width;

        [SerializeField] private int height;

        public int Height => this.height;

        public int Size => this.Width * this.Height;

        public Vector2Int Shape => new Vector2Int(this.Width, this.Height);

        public int Width
        {
            get => this.width;
            set => this.width = value;
        }

        public int this[int x, int y]
        {
            get => this.items[x, y];
            set => this.items[x, y] = value;
        }

        public int this[Vector2Int position]
        {
            get => this[position.x, position.y];
            set => this[position.x, position.y] = value;
        }

        public Grid2D()
        {
        }

        public Grid2D(int[,] items)
        {
            this.Width = items.GetLength(0);
            this.height = items.GetLength(1);

            this.items = items;
        }

        public Grid2D(int width, int height)
        {
            this.Width = width;
            this.height = height;

            this.items = new int[width, height];
        }

        public Grid2D(Vector2Int shape)
            : this(shape.x, shape.y)
        {
        }

        public Grid2D CopyShape() => new Grid2D(this.Width, this.Height);

        public bool Contains(Vector2Int position)
        {
            return position.x >= 0 && position.x < this.Width &&
                   position.y >= 0 && position.y < this.Height;
        }

        public GridNeighborhood GetRegion(int x, int y, int radius) => new GridNeighborhood(this, x, y, radius);

        public Texture2D ToTexture(IColorScheme colorScheme, FilterMode filterMode = FilterMode.Point)
        {
            var texture = new Texture2D(this.Width, this.Height)
            {
                filterMode = filterMode
            };

            for (var x = 0; x < this.Width; x++)
            for (var y = 0; y < this.Height; y++)
            {
                var item = this[x, y];
                var color = colorScheme[item];

                texture.SetPixel(x, y, color);
            }

            texture.Apply();
            return texture;
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (var x = 0; x < this.Width; x++)
            for (var y = 0; y < this.Height; y++)
                yield return this[x, y];
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}