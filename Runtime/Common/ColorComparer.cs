namespace Chinchillada
{
    using System;
    using UnityEngine;

    public interface IColorComparer
    {
        bool AreAlmostEqual(Color first, Color second);
    }

    [Serializable]
    public class ColorComparer : IColorComparer
    {
        [SerializeField] private float allowedDifference = 0.05f;

        public bool AreAlmostEqual(Color first, Color second)
        {
            return this.Compare(first.r, second.r)
                && this.Compare(first.g, second.g)
                && this.Compare(first.b, second.b);
        }

        private bool Compare(float a, float b)
        {
            var difference = Mathf.Abs(a - b);
            return difference <= this.allowedDifference;
        }
    }
}