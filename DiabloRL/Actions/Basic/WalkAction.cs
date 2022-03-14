using System;
using DiabloRL.Entities;
using GoRogue.GameFramework;
using SadConsole.Entities;
using SadRogue.Integration;
using SadRogue.Primitives;

namespace DiabloRL.Actions.Basic
{
    public class WalkAction : Action
    {
        public WalkAction(GameEntity entity, Direction dir, bool checkForCancel = false) : base(entity)
        {
            _direction = dir;
            _checkForCancel = checkForCancel;
        }

        protected override ActionResult OnProcess()
        {
            var newPos = GameEntity.Position + _direction;

            if (!GameEntity.CanMoveIn(_direction))
            {
                var target = Map.GetEntityAt<GameEntity>(newPos);
                if (target == null)
                    return Fail($"{GameEntity.Name} cannot move from {GameEntity.Position} to {newPos}");
                
                // otherwise attack the target
                return new AttackAction(GameEntity, target);
                // return Fail($"{GameEntity.Name} attacks {target.Name}");
            }

            // Entity can move to the new position
            GameEntity.Position = newPos;

            return ActionResult.Done;
        }

        private Direction _direction;
        private bool _checkForCancel;
    }
}