using System;
using System.Collections.Generic;
using DiabloRL.Scripts.Cartography.Tiles;
using Godot;
using GoRogue.Components.ParentAware;
using GoRogue.GameFramework;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Components;

public partial class PlayerInputComponent : ParentAwareComponentBase<DiabloGameObject> {

    private Dictionary<Key, Direction> _keyMappings = new() {
        {Key.Kp8, Direction.Up},
        {Key.Kp2, Direction.Down},
        {Key.Kp4, Direction.Left},
        {Key.Kp6, Direction.Right},
        {Key.Kp7, Direction.UpLeft},
        {Key.Kp9, Direction.UpRight},
        {Key.Kp1, Direction.DownLeft},
        {Key.Kp3, Direction.DownRight}
    };
    
    public Direction GetMovement(InputEvent inputEvent) {
        var movement = Direction.None;
        
        if (inputEvent is InputEventKey inputEventKey) {
            foreach (var keyMapping in _keyMappings) {
                if (keyMapping.Key == inputEventKey.Keycode) {
                    movement = keyMapping.Value;
                }
            }
            // var x = Convert.ToInt16(inputEventKey.Keycode == Key.D) - Convert.ToInt16(inputEventKey.Keycode == Key.A);
            // var y = Convert.ToInt16(inputEventKey.Keycode == Key.S) - Convert.ToInt16(inputEventKey.Keycode == Key.W);
            // movement = new Point(x, y);
        }

        return movement;
    }
}