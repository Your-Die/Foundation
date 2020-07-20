using System;

namespace Chinchillada.Grid.Visualization
{
    public interface IGridRenderer
    {
        Grid2D Grid { get; }

        event Action<Grid2D> NewGridRegistered;


        /// <summary>
        /// Render the <paramref name="grid"/>.
        /// </summary>
        void Render(Grid2D grid);
    }
}

