using System;
using SadRogue.Primitives;

namespace DiabloRL.Extensions;

public static class DirectionExtensions
{
    private static Direction[] _directions = new[]
    {
        Direction.DownLeft,
        Direction.Down,
        Direction.DownRight,
        Direction.Left,
        Direction.Right,
        Direction.UpLeft,
        Direction.Up,
        Direction.UpRight
    };
    
    public static Direction RandomDirection()
    {
        var rand = new Random((int) DateTime.UtcNow.Ticks);
        var randomDirIndex = rand.Next(8);
        return _directions[randomDirIndex];
    }
}