namespace Chinchillada
{
    using System.Collections.Generic;

    public interface ICoordinateSpace<TValue, TCoordinate>
    {
        IEnumerable<TCoordinate> Coordinates { get; }

        TValue Get(TCoordinate coordinate);

        void Set(TCoordinate coordinate, TValue value);
    }
}