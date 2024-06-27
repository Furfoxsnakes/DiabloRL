using System.Threading.Tasks;
using DiabloRL.Scripts.Cartography.Tiles;
using DiabloRL.Scripts.Processing.Things;
using Godot;

namespace DiabloRL.Scripts.Processing.Actions;

public partial class AttackAction : Action {
    private Attack _attack;
    protected DiabloEntity Target;
    
    public AttackAction(DiabloEntity entity, DiabloEntity target, Attack attack) : base(entity) {
        _attack = attack;
        Target = target;
    }

    protected override ActionResult OnProcess() {
        var damage = Target.ReceiveDamage(_attack);
        return ActionResult.Done;
    }
}