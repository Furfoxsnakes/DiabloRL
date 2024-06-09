using Godot;

namespace DiabloRL.Scripts.Common.States;

public abstract partial class State : Node {
    public bool IsComplete => _isComplete;
    private bool _isComplete;
    protected Statemachine Statemachine;
    
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Do(float delta);
    public abstract void PhysicsDo(float delta);
    public abstract void HandleInput(InputEvent inputEvent);

    public void Init(Statemachine statemachine) {
        Statemachine = statemachine;
        _isComplete = false;
    }

    public void Complete() {
        _isComplete = true;
    }
}