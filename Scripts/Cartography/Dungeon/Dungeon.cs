using System.Collections.Generic;
using System.Linq;
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

    public DiabloEntity PlayerEntity;
    public Map Map;
    public List<DiabloEntity> Entities = new List<DiabloEntity>(); 

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

    public void AddEntity(DiabloEntity entity) {
        if (Map.Entities.Contains(entity)) return;
        
        AddDiabloGameObject(entity);
        Entities.Add(entity);
        Map.AddEntity(entity);
    }

    public void RemoveEntity(DiabloEntity entity) {
        if (!Map.Entities.Contains(entity)) return;
        
        OnFOVClaculated -= entity.OnPlayerFovCalcuated;
        
        Map.RemoveEntity(entity);
        Entities.Remove(entity);
        entity.QueueFree();
    }

    public void CalculatePlayerFov() {
        Map.PlayerFOV.Calculate(PlayerEntity.Position, 5, Distance.Euclidean);
        // Game.Instance.Camera.GlobalPosition = PlayerEntity.GlobalPosition;
        OnFOVClaculated?.Invoke(Map.PlayerFOV);
    }
}