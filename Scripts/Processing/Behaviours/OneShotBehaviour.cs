namespace DiabloRL.Scripts.Processing.Behaviours;

public partial class OneShotBehaviour : Behaviour {

    private Action _action;

    public override bool NeedsUserInput => _action == null;

    public OneShotBehaviour(Action action) {
        _action = action;
    }

    public override Action NextAction() {
        var action = _action;
        _action = null;
        return action;
    }
}