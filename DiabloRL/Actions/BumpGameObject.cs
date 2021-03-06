using System;
using DiabloRL.Actors;
using GoRogue.GameFramework;
using SadConsole.Actions;

namespace DiabloRL.Actions
{
    public class BumpGameObject : ActionBase<Actor, GameObject>
    {
        public BumpGameObject(Actor source, GameObject target) : base(source, target)
        {
        }

        public override void Run(TimeSpan timeElapsed)
        {
            System.Console.WriteLine($"{Source} bumps {Target}");
        }
    }
}