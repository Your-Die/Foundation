using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada
{
    public interface IVector2IntNeighborhood
    {
        IEnumerable<Vector2Int> GetNeighbors(Vector2Int vector, int radius = 1);
    }
}