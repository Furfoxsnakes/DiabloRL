using DiabloRL.Actors;
using DiabloRL.Containers;
using GoRogue;
using SadConsole;
using SadConsole.Actions;
using SadConsole.Components.GoRogue;

namespace DiabloRL.Actions
{
    public class MoveToPlayer : GameFrameProcessor<Monster>
    {
        public override void ProcessGameFrame()
        {
            var player = Parent.CurrentMap.ControlledGameObject;
            var moveDirection = Direction.GetDirection(Parent.Position, player.Position);
            var didMove = Parent.MoveIn(moveDirection);
            if (!didMove)
            {
                var target = (Parent.CurrentMap as DungeonMap)?.ControlledGameObject;
                var action = new BasicMoveAndAttackAction(Parent, target, moveDirection);
                var playingScreen = Global.CurrentScreen as PlayingScreen;
                playingScreen?.MapConsole.Actions.Push(action);
            }
        }
    }
}