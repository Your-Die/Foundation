using System;
using System.Collections.Generic;
using System.Linq;
using Chinchillada.Foundation;
using UnityEngine;

namespace Chinchillada.Grid
{
    public interface IWindowSampler<T>
    {
        T Sample(IGrid2D<T> grid, int x, int y, Vector2Int windowShape);
    }

    public abstract class WindowSequenceSampler<T> : IWindowSampler<T>
    {
        public T Sample(IGrid2D<T> grid, int x, int y, Vector2Int windowShape)
        {
            var window = grid.GetWindow(x, y, windowShape);
            return this.Sample(window);
        }

        protected abstract T Sample(IEnumerable<T> window);
    }

    [Serializable]
    public class MeanSampler : WindowSequenceSampler<float>
    {
        protected override float Sample(IEnumerable<float> window) => window.Average();
    }

    [Serializable]
    public class RoundedMeanSampler : WindowSequenceSampler<int>
    {
        protected override int Sample(IEnumerable<int> window) => (int) window.Average();
    }

    [Serializable]
    public class ModeSampler<T> : WindowSequenceSampler<T>
    {
        protected override T Sample(IEnumerable<T> window) => window.Mode();
    }

    [Serializable]
    public class RandomSampler<T> : WindowSequenceSampler<T>
    {
        protected override T Sample(IEnumerable<T> window) => window.ChooseRandom();
    }
}