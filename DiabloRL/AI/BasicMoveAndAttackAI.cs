using System.Linq;
using GoRogue.GameFramework;
using SadRogue.Integration;
using SadRogue.Primitives;

namespace DiabloRL.AI
{
    internal class BasicMoveAndAttackAI : EnemyAI
    {
        public override void TakeTurn()
        {
            if (Parent?.CurrentMap == null) return;
            if (!Parent.CurrentMap.PlayerFOV.CurrentFOV.Contains(Parent.Position)) return;
            
            var path = Parent.CurrentMap.AStar.ShortestPath(Parent.Position, Game.GameScreen.Player.Position);
            if (path == null) return;
            var firstPoint = path.GetStep(0);
            
            if (Parent.CanMove(firstPoint))
            {
                Game.GameScreen.MessageLog.AddMessage(
                    $"An enemy moves {Direction.GetDirection(Parent.Position, firstPoint)}!");
                Parent.Position = firstPoint;
                return;
            }
            
            // attack the player
            var target = Parent.CurrentMap.GetEntityAt<RogueLikeEntity>(firstPoint);
            if (target == null) return; // bumped terrain
            Game.GameScreen.MessageLog.AddMessage($"Enemy attacks the Player! Oof!");
        }
    }
}