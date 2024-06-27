using DiabloRL.Scripts.Cartography.Tiles;
using DiabloRL.Scripts.Processing.Things;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Processing.Actions;

public class MeleeAttackAction : AttackAction {
    public MeleeAttackAction(DiabloEntity entity, DiabloEntity target, Attack attack) : base(entity, target, attack) {
        
    }

    protected override ActionResult OnProcess() {
        var direction = Direction.GetDirection(DiabloEntity.Position, Target.Position);
        Game.Instance.DoBumpTweenByDirection(DiabloEntity, direction);
        return base.OnProcess();
    }
}