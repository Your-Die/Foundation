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
        public static Vector2Int ToVectorInt(this Direction direction)
        {
            switch (direction)
            {
                case Direction.North: return Vector2Int.up;
                case Direction.East: return Vector2Int.right;
                case Direction.South: return Vector2Int.down;
                case Direction.West: return Vector2Int.left;
                default: throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
        
        public static Vector2 ToVector(this Direction direction)
        {
            switch (direction)
            {
                case Direction.North: return Vector2.up;
                case Direction.East:  return Vector2.right;
                case Direction.South: return Vector2.down;
                case Direction.West: return Vector2.left;
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