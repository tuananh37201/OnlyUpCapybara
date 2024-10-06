using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    Forward,
    Back
}

public static class DirectionExtensions
{
    public static Vector3 ToVector3(this Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return new Vector3(0, 1, 0);
            case Direction.Down:
                return new Vector3(0, -1, 0);
            case Direction.Left:
                return new Vector3(-1, 0, 0);
            case Direction.Right:
                return new Vector3(1, 0, 0);
            case Direction.Forward:
                return new Vector3(0, 0, 1);
            case Direction.Back:
                return new Vector3(0, 0, -1);
            default:
                return Vector3.zero;
        }
    }
}
