using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada
{
    [Serializable]
    public class OrthogonalNeighborhood : IVector2IntNeighborhood
    {
        public IEnumerable<Vector2Int> GetNeighbors(Vector2Int vector, int radius = 1)
        {
            for (var i = 1; i <= radius; i++)
            {
                yield return vector + Vector2Int.up    * i;
                yield return vector + Vector2Int.right * i;
                yield return vector + Vector2Int.down  * i;
                yield return vector + Vector2Int.left  * i;
            }
        }
    }
}