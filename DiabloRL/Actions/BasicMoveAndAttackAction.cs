using System;
using DiabloRL.Actors;
using GoRogue;
using SadConsole.Actions;

namespace DiabloRL.Actions
{
    public class BasicMoveAndAttackAction : ActionBase<Actor, Actor>
    {
        private Direction _dir;
        
        public BasicMoveAndAttackAction(Actor source, Actor target, Direction dir) : base(source, target)
        {
            _dir = dir;
        }

        public override void Run(TimeSpan timeElapsed)
        {
            var didMove = Source.MoveIn(_dir);
            if (didMove)
            {
                Finish(new ActionResult(true));
                return;
            }

            var actor = Source.CurrentMap.GetEntity<Actor>(Source.Position + _dir);
            if (actor == Target)
                Target.TakeDamage(1);
            
            Finish(new ActionResult(true));
        }
    }
}