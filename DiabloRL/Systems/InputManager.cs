using DiabloRL.Actions;
using DiabloRL.Actors;
using GoRogue;

namespace DiabloRL.Systems
{
    public class InputManager
    {
        public bool IsPlayerTurn = true;

        public bool MovePlayer(Player player, Direction dir)
        {
            if (!IsPlayerTurn) return false;

            IsPlayerTurn = false;

            var didMove = player.MoveIn(dir);

            if (!didMove)
            {
                var newPos = player.Position + dir;
                var monster = Game.MapScreen.Map.GetEntity<Monster>(newPos);

                if (monster == null)
                {
                    // bumped into a wall or door mayhaps?
                }
                else
                {
                    var bumpAction = new BasicMoveAndAttackAction(player, monster, dir);
                    Game.MapScreen.Actions.Push(bumpAction);
                }
            }

            Game.MapScreen.GameFrameManager.RunLogicFrame = true;

            return true;
        }
    }
}