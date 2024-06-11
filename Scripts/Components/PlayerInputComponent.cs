using System;
using DiabloRL.Scripts.Cartography.Tiles;
using Godot;
using GoRogue.Components.ParentAware;
using GoRogue.GameFramework;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Components;

public partial class PlayerInputComponent : ParentAwareComponentBase<DiabloGameObject> {
    public Point GetMovement(InputEvent inputEvent) {
        var movement = Point.None;
        if (inputEvent is InputEventKey inputEventKey) {
            var x = Convert.ToInt16(inputEventKey.Keycode == Key.D) - Convert.ToInt16(inputEventKey.Keycode == Key.A);
            var y = Convert.ToInt16(inputEventKey.Keycode == Key.S) - Convert.ToInt16(inputEventKey.Keycode == Key.W);
            movement = new Point(x, y);
        }

        return movement;
    }
}