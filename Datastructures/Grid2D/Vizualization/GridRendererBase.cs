using System;
using Chinchillada.Foundation;

namespace Chinchillada.Grid.Visualization
{
    public abstract class GridRendererBase : ChinchilladaBehaviour, IGridRenderer
    {
        public Grid2D Grid { get; private set; }
        
        public event Action<Grid2D> NewGridRegistered;

        public void Render(Grid2D grid)
        {
            this.Grid = grid;
            this.NewGridRegistered?.Invoke(grid);
            
            this.RenderGrid(grid);
        }

        protected abstract void RenderGrid(Grid2D newGrid);
    }
}