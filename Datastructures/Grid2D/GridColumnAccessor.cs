namespace Chinchillada.Grid
{
    public class GridColumnAccessor : GridListAccessor
    {
        public int ColumnIndex { get; set; }

        public override int Count => this.Grid.Height;

        public override int this[int index]
        {
            get => this.Grid[this.ColumnIndex, index];
            set => this.Grid[this.ColumnIndex, index] = value;
        }

        public GridColumnAccessor(Grid2D grid) : base(grid)
        {
        }
    }
}