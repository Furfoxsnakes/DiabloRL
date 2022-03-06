using System;
using DiabloRL.Actions.Basic;
using DiabloRL.Behaviors;
using DiabloRL.Components;
using GoRogue.Components;
using SadRogue.Integration;
using SadRogue.Integration.Keybindings;
using SadRogue.Primitives;

namespace DiabloRL.Entities
{
    public class Player : GameEntity
    {
        public Player() : base(foreground: Color.Yellow, background: Color.Black, glyph: '@', walkable: false, transparent: false, 1)
        {
            // Add component for controlling player movement via keyboard.  Other (non-movement) keybindings can be
            // added as well
            var motionControl = new PlayerKeyboardControls();
            motionControl.SetMotions(PlayerKeybindingsComponent.ArrowMotions);
            motionControl.SetMotions(PlayerKeybindingsComponent.NumPadAllMotions);
            AllComponents.Add(motionControl);

            // Add component for updating map's player FOV as they move
            AllComponents.Add(new PlayerFOVController());
        }
    }
}