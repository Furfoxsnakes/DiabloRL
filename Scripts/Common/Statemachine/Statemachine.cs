using DiabloRL.Scripts.Common.States;
using Godot;

namespace DiabloRL.Scripts.Common;

public abstract partial class Statemachine : Node {
    
    [Export] public abstract Node Owner { get; set; }
    public State CurrentState => _currentState;
    private State _currentState;

    public override void _Ready() {
        foreach (var child in GetChildren()) {
            if (child is State state) {
                state.Init(this);
            }
        }
    }

    public override void _Process(double delta) {
        _currentState?.Do((float)delta);
    }

    public override void _PhysicsProcess(double delta) {
        _currentState?.PhysicsDo((float)delta);
    }

    public override void _UnhandledKeyInput(InputEvent @event) {
        if (!@event.IsPressed()) return;
        
        _currentState?.HandleInput(@event);
    }

    public void SetState(State state, bool forceReset = false) {
        if (state == _currentState && !forceReset) return;
        
        _currentState?.Exit();
        _currentState = state;
        _currentState.Init(this);
        _currentState.Enter();
    }
}