using System;
using UnityEngine;

namespace Chinchillada.Foundation
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public static class DirectionExtensions
    {
        public static Vector2 ToVector(this Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return new Vector2(0, 1);
                case Direction.East:
                    return new Vector2(1, 0);
                case Direction.South:
                    return new Vector2(0, -1);
                case Direction.West:
                    return new Vector2(-1, 0);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public static Direction Inverse(this Direction direction)
        {
            switch (direction)
            {
                case Direction.North: return Direction.South;
                case Direction.East: return Direction.West;
                case Direction.South: return Direction.North;
                case Direction.West: return Direction.East;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

        }
        
        public static Direction Clockwise(this Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.East;
                case Direction.East:
                    return Direction.South;
                case Direction.South:
                    return Direction.West;
                case Direction.West:
                    return Direction.North;

                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public static Direction CounterClockwise(this Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.West;
                case Direction.East:
                    return Direction.North;
                case Direction.South:
                    return Direction.East;
                case Direction.West:
                    return Direction.South;

                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }   
        }
    }
}