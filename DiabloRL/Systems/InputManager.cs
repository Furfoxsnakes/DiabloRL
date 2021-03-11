using DiabloRL.Actions;
using DiabloRL.Actors;
using DiabloRL.Containers;
using GoRogue;
using SadConsole;

namespace DiabloRL.Systems
{
    public class InputManager
    {    
        public bool IsPlayerTurn = true;
        public static string PlayerDidMoveNotification = "PlayerDidMove";

        public bool MovePlayer(Player player, Direction dir)
        {
            if (!IsPlayerTurn) return false;

            IsPlayerTurn = false;

            var didMove = player.MoveIn(dir);

            if (!didMove)
            {
                var newPos = player.Position + dir;
                // var monster = Game.MapConsole.Map.GetEntity<Monster>(newPos);
                var monster = player.CurrentMap.GetEntity<Monster>(newPos);

                if (monster == null)
                {
                    // bumped into a wall or door mayhaps?
                }
                else
                {
                    var bumpAction = new BasicMoveAndAttackAction(player, monster, dir);
                    var playingScreen = Global.CurrentScreen as PlayingScreen;
                    playingScreen?.MapConsole.Actions.Push(bumpAction);
                }
            }

            // Game.MapConsole.GameFrameManager.RunLogicFrame = true;
            // _gameFrameManager.RunLogicFrame = true;
            this.PostNotification(PlayerDidMoveNotification, player);

            return true;
        }
    }
}