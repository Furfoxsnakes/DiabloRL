using DiabloRL.Entities;

namespace DiabloRL.Actions.Basic;

public class AttackAction : Action
{
    public AttackAction(GameEntity attacker, GameEntity defender) : base(attacker)
    {
        _defender = defender;
    }

    protected override ActionResult OnProcess()
    {
        if (GameEntity is Enemy && _defender is Enemy)
            return Fail($"Enemies can't attack each other, ya dingus.");

        _defender.TakeHit(1);
        Log($"{_defender.Name} now has {_defender.Life.Current}/{_defender.Life.Max} life.");
        return ActionResult.Done;
    }

    private GameEntity _defender;
}