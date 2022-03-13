using System;
using DiabloRL.Entities;
using SadConsole.Actions;

namespace DiabloRL.Actions.Basic;

public class GoRogueAttackAction : ActionBase<GameEntity, GameEntity>
{
    public GoRogueAttackAction(GameEntity source, GameEntity target) : base(source, target)
    {
    }

    public override void Run(TimeSpan timeElapsed)
    {
        if (Source is Enemy && Target is Enemy)
        {
            // Monster is trying to attack another monster
            Finish(SadConsole.Actions.ActionResult.Failure);
            return;
        }

        Finish(SadConsole.Actions.ActionResult.Success);
    }

    protected override void OnSuccessResult()
    {
        base.OnSuccessResult();
        System.Console.WriteLine($"{Source.Name} attacks {Target.Name}");
    }
}