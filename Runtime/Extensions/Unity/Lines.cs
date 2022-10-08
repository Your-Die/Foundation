using System.Collections.Generic;
using UnityEngine;

public static class Lines
{
    public static IEnumerable<Vector2Int> GetPointsOnLine(Vector2Int from, Vector2Int to)
    {
        var dy = to.y - from.y;
        var dx = to.x - from.x;

        int stepX, stepY;

        if (dy < 0)
        {
            dy    = -dy;
            stepY = -1;
        }
        else
        {
            stepY = 1;
        }

        if (dx < 0)
        {
            dx    = -dx;
            stepX = -1;
        }
        else
        {
            stepX = 1;
        }

        dy <<= 1;
        dx <<= 1;

        float fraction;

        yield return from;
        if (dx > dy)
        {
            fraction = dy - (dx >> 1);
            while (Mathf.Abs(from.x - to.x) > 1)
            {
                if (fraction >= 0)
                {
                    from.y   += stepY;
                    fraction -= dx;
                }

                from.x   += stepX;
                fraction += dy;

                yield return from;
            }
        }
        else
        {
            fraction = dx - (dy >> 1);
            while (Mathf.Abs(from.y - to.y) > 1)
            {
                if (fraction >= 0)
                {
                    from.x   += stepX;
                    fraction -= dy;
                }

                from.y   += stepY;
                fraction += dx;
                yield return from;
            }
        }
    }
}