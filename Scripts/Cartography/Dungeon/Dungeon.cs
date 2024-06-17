using DiabloRL.Scripts.Cartography.Tiles;
using DiabloRL.Scripts.Common;
using DiabloRL.Scripts.Common.States.DungeonStates;
using Godot;
using GoRogue.FOV;
using GoRogue.GameFramework;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Cartography.Dungeon;

public partial class Dungeon : Node {
    [Export] private DungeonStateMachine _statemachine;
    [ExportCategory("States")]
    [Export] private DungeonState _generateState;
    [Export] private DungeonState _exploreState;

    public DiabloGameObject PlayerObject;
    public Map Map;

    public delegate void FOVCalculated(IFOV fov);
    public event FOVCalculated OnFOVClaculated;

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

    public void AddDiabloGameObject(DiabloGameObject dgo) {
        AddChild(dgo);
        OnFOVClaculated += dgo.OnPlayerFovCalcuated;
    }

    public void CalculatePlayerFov() {
        Map.PlayerFOV.Calculate(PlayerObject.Position, 5, Distance.Euclidean);
        OnFOVClaculated?.Invoke(Map.PlayerFOV);
    }
}