using DiabloRL.Entities;
using DiabloRL.Things;
using SadRogue.Primitives;

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
            // Monsters cannot damage each other
            // return Fail($"Enemies can't attack each other, ya dingus.");
            return ActionResult.Fail;

        var attack = GameEntity.GetAttack(_defender);
        
        // send the hit to the defender
        var direction = Direction.GetDirection(_defender.Position, GameEntity.Position);
        var hit = new Hit(GameEntity, attack, true, direction);
        _defender.TakeHit(this,hit);
        
        return ActionResult.Done;
    }

    private GameEntity _defender;
}