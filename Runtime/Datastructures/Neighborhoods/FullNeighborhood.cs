using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada
{
    [Serializable]
    public class FullNeighborhood : IVector2IntNeighborhood
    {
        public IEnumerable<Vector2Int> GetNeighbors(Vector2Int vector, int radius = 1)
        {
            for (var x = -radius; x <= radius; x++)
            for (var y = -radius; y <= radius; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                yield return vector + new Vector2Int(x, y);
            }
        }
    }
}