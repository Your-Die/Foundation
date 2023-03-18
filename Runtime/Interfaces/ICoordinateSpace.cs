using System;
using System.Linq;

namespace Chinchillada
{
    using System.Collections.Generic;

    public interface ICoordinateSpace<TValue, TCoordinate>
    {
        IEnumerable<TCoordinate> Coordinates { get; }

        TValue Get(TCoordinate coordinate);

        void Set(TCoordinate coordinate, TValue value);

        IEnumerable<TCoordinate> GetNeighbors(TCoordinate coordinate);
    }

    public static class CoordinateSpaceExtensions
    {
        public static IEnumerable<TCoordinate> CoordinatesWhere<TValue, TCoordinate>(
            this ICoordinateSpace<TValue, TCoordinate> space,
            Func<TValue, bool> selector)
        {
            return space.Coordinates.Where(MatchesSelector);

            bool MatchesSelector(TCoordinate coordinate)
            {
                var value = space.Get(coordinate);
                return selector.Invoke(value);
            }
        }
    }
}