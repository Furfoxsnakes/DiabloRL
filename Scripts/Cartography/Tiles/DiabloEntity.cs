using System.Collections.Generic;
using DiabloRL.Scripts.Processing;
using DiabloRL.Scripts.Processing.Behaviours;
using Godot;
using GoRogue.FOV;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Cartography.Tiles;

public partial class DiabloEntity : DiabloGameObject {

    private Behaviour _behaviour;
    public Behaviour Behaviour => _behaviour;

    public DiabloEntity(Point pos, GameObjectDetails details) : base(pos, details, 1) {
        
    }
    
    public override void OnPlayerFovCalcuated(IFOV fov) {
        Visible = fov.BooleanResultView[Position];
    }

    public void SetBehaviour(Behaviour behaviour) {
        _behaviour = behaviour;
    }

    public void SetNextAction(Action action) {
        SetBehaviour(new OneShotBehaviour(action));
    }

    public IEnumerable<Action> TakeTurn() {
        var turnAction = _behaviour.NextAction();
        yield return turnAction;
    }
}