using DiabloRL.Actions.Basic;
using DiabloRL.AI;
using DiabloRL.Behaviors;
using DiabloRL.Entities;
using GoRogue.GameFramework;
using SadRogue.Integration;
using SadRogue.Integration.Keybindings;
using SadRogue.Primitives;

namespace DiabloRL
{
    /// <summary>
    /// Subclass of the integration library's keybindings component that moves enemies as appropriate when the player
    /// moves.
    /// </summary>
    /// <remarks>
    /// CUSTOMIZATION: Components can also be attached to maps, so the code for calling TakeTurn on all entities could
    /// be moved to a map component as well so that it is more re-usable by code that doesn't pertain to movement.
    /// </remarks>
    internal class PlayerKeyboardControls : PlayerKeybindingsComponent
    {
        protected override void MotionHandler(Direction direction)
        {
            var player = Parent as Player;
            player.SetBehavior(new OneShotBehavior(new WalkAction(player, direction)));

            if (!Game.GameScreen.Player.Behavior.NeedsUserInput)
                Game.GameScreen.Process();
        }
    }
}