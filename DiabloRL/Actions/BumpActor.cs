using System;
using DiabloRL.Actors;
using GoRogue.GameFramework;
using SadConsole;
using SadConsole.Actions;

namespace DiabloRL.Actions
{
    public class BumpActor : ActionBase<Actor, Actor>
    {
        public BumpActor(Actor source, Actor target) : base(source, target)
        {
            
        }

        public override void Run(TimeSpan timeElapsed)
        {
            Target.TakeDamage(1);
            Finish(new ActionResult(true));
        }
    }
}