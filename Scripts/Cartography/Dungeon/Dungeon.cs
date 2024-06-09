using DiabloRL.Scripts.Cartography.Tiles;
using DiabloRL.Scripts.Common;
using DiabloRL.Scripts.Common.States.DungeonStates;
using Godot;
using GoRogue.GameFramework;

namespace DiabloRL.Scripts.Cartography.Dungeon;

public partial class Dungeon : Node {
    [Export] private DungeonStateMachine _statemachine;
    [ExportCategory("States")]
    [Export] private DungeonState _generateState;
    [Export] private DungeonState _exploreState;

    public DiabloGameObject PlayerObject;
    public Map Map;

    public override void _Ready() {
        _statemachine.SetState(_generateState);
    }

    public override void _Process(double delta) {
        if (_statemachine.CurrentState.IsComplete) {
            HandleNextState();
        }
    }

    private void HandleNextState() {
        if (_statemachine.CurrentState == _generateState) {
            _statemachine.SetState(_exploreState);
        }
    }
}