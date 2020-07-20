using System.Collections.Generic;
using Chinchillada.Foundation;
using UnityEngine;

namespace Chinchillada.Grid
{
    public interface IGrid2D<T>
    {
        int Height { get; }
        int Width { get; set; }
        T this[int x, int y] { get; set; }
        T this[Vector2Int position] { get; set; }
    }

    public static class GridExtensions
    {

        
        public static IEnumerable<(Vector2Int, Direction)> GetNeighbors<T>(this IGrid2D<T> grid, Vector2Int node)
        {
            if (node.x > 0) yield return (new Vector2Int(node.x - 1, node.y), Direction.West);
            if (node.y > 0) yield return (new Vector2Int(node.x, node.y - 1), Direction.South);
            if (node.x < grid.Width - 1) yield return (new Vector2Int(node.x + 1, node.y), Direction.East);
            if (node.y < grid.Height - 1) yield return (new Vector2Int(node.x, node.y + 1), Direction.South);
        }

        public static IEnumerable<T> GetWindow<T>(this IGrid2D<T> grid, int topLeftX, int topLeftY, Vector2Int window)
        {
            for (var x = 0; x < window.x; x++)
            for (var y = 0; y < window.y; y++)
            {
                var windowX = topLeftX + x;
                var windowY = topLeftY + y;

                yield return grid[windowX, windowY];
            }
        }

        public static IEnumerable<Vector2Int> GetCoordinates<T>(this IGrid2D<T> grid)
        {
            for (var x = 0; x < grid.Width; x++)
            for (var y = 0; y < grid.Height; y++)
                yield return new Vector2Int(x, y);
        }
    }
}