namespace Chinchillada
{
    using System;
    using UnityEngine;
    using UnityEngine.Assertions;

    public class RectMapper
    {
        private readonly Rect    from;
        private readonly RectInt to;

        public RectMapper(Rect from, RectInt to)
        {
            this.from = from;
            this.to   = to;
        }

        public RectInt MapRect(Rect rect)
        {
            var min = this.MapVector(rect.min);
            var max = this.MapVector(rect.max);

            var size = max - min;

            return new RectInt(min, size);
        }

        public Vector2Int MapVector(Vector2 vector)
        {
            return new Vector2Int
            {
                x = this.MapX(vector.x),
                y = this.MapY(vector.y)
            };
        }

        public int MapX(float x)
        {
            var percentage = Mathf.InverseLerp(this.from.xMin, this.from.xMax, x);
            return (int)Mathf.Lerp(this.to.xMin, this.to.xMax, percentage);
        }

        public int MapY(float y)
        {
            var percentage = Mathf.InverseLerp(this.from.yMin, this.from.yMax, y);
            return (int)Mathf.Lerp(this.to.yMin, this.to.yMax, percentage);
        }
    }
}