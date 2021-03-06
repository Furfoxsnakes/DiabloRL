using DiabloRL.Actors;
using GoRogue;
using Microsoft.Xna.Framework.Input;

namespace DiabloRL.Systems
{
    public class InputManager
    {
        private bool _isPlayerTurn = true;

        public bool MovePlayer(Player player, Direction dir)
        {
            if (!_isPlayerTurn) return false;

            _isPlayerTurn = false;

            var didMove = player.MoveIn(dir);

            if (!didMove)
            {
                var newPos = player.Position + dir;
                var monster = Game.MapScreen.Map.GetEntity<Monster>(newPos);

                if (monster == null) return didMove;
                
                monster.TakeDamage(2);
            }

            ActivateMonsters();

            return true;
        }

        private void ActivateMonsters()
        {
            foreach (var monster in Game.MapScreen.Map.Monsters)
            {
                var directionToPlayer =
                    Direction.GetDirection(monster.Position, Game.MapScreen.Map.ControlledGameObject.Position);
                var didMove = monster.MoveIn(directionToPlayer);

                if (!didMove)
                {
                    var player = monster.CurrentMap.GetEntity<Player>(monster.Position + directionToPlayer);
                    if (player != null)
                        player.TakeDamage(1);
                }
            }

            _isPlayerTurn = true;
        }
    }
}