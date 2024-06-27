using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using DiabloRL.Scripts.Cartography.Dungeon;
using DiabloRL.Scripts.Cartography.Tiles;
using DiabloRL.Scripts.Cartography.Tiles.Entities;
using DiabloRL.Scripts.Processing;
using SadRogue.Primitives;
using Action = DiabloRL.Scripts.Processing.Action;

public partial class Game : Node {
    [Export] private Dungeon _dungeon;
    public Dungeon Dungeon => _dungeon;

    [Export] private Camera2D _camera;
    public Camera2D Camera => _camera;

    private static Game _instance;
    public static Game Instance => _instance;
    
    private IEnumerator<GameResult> _processEnumerator;

    private DiabloEntity _actingEntity;
    public DiabloEntity ActingEntity => _actingEntity;

    private DiabloEntity _cancelEntity;
    public DiabloEntity CancelEntity => _cancelEntity;

    private bool _isTweening = false;

    public override void _EnterTree() {
        if (_instance == null) {
            _instance = this;
        }
    }

    public GameResult ProcessGame() {
        if (_processEnumerator == null) {
            _processEnumerator = CreateProcessEnumerable().GetEnumerator();
        }

        if (!_processEnumerator.MoveNext()) {
            return new GameResult(GameResultFlags.GameOver | GameResultFlags.NeedsPause);
        }

        return _processEnumerator.Current;
    }

    private IEnumerable<GameResult> CreateProcessEnumerable() {

        while (true) {
            
            while (!_isTweening) {
            
                // if (Dungeon.Entities.Count == 0) continue;
            
                for (var entityIndex = 0; entityIndex < Dungeon.Entities.Count; entityIndex++) {
                    var entity = Dungeon.Entities[entityIndex];
                
                    // GD.Print($"{entity.Name} has {entity.Energy.Current} energy");

                    while (entity.Energy.HasEnoughEnergyForAction) {
                        while (entity.Behaviour.NeedsUserInput) {
                            yield return new GameResult(GameResultFlags.NeedsUserInput, entity);
                        }
                
                        foreach (var action in entity.TakeTurn()) {
                            foreach (var result in ProcessAction(action)) {
                                yield return result;
                            }
                        }
                    }

                    if (!(entity is Player) && !entity.IsAlive) {
                        // don't process this entity since it's been killed
                        // go back 1 entity
                        entityIndex--;
                    } 
                }
            
                Dungeon.Entities.ForEach(entity => entity.Energy.Gain());
            }

            // allows the IEnumerator to keep running to process tweens
            // not entirely sure why it needs to be NeedsUserInput, but it works!
            yield return new GameResult(GameResultFlags.NeedsUserInput);
        }
    }

    private IEnumerable<GameResult> ProcessAction(Action actionToProcess) {
        var actions = new Queue<Action>();
        actions.Enqueue(actionToProcess);

        while (actions.Count > 0) {
            var action = actions.Peek();
            
            _actingEntity = action.DiabloEntity;

            var result = action.Process(actions);

            while (result.AlternateAction != null) {
                result = result.AlternateAction.Process(actions);
            }

            if (result.IsDone) actions.Dequeue();

            if (result.Success) action.AfterSuccess();

            _actingEntity = null;
            
            Dungeon.CalculatePlayerFov();

            if (result.NeedsPause) {
                GD.Print("Waiting for input");
                yield return new GameResult(GameResultFlags.NeedsPause);
            } else if (result.NeedsCheckForCancel) {
                _cancelEntity = action.DiabloEntity;
                yield return new GameResult(GameResultFlags.CheckForCancel);
            }
        }
    }

    public void DoBumpTweenByDirection(DiabloEntity entity, Direction direction) {
        _isTweening = true;

        var tween = GetTree().CreateTween();
        
        var currentGlobalPos = entity.GlobalPosition;
        var newGlobalPos = currentGlobalPos + (new Vector2(direction.DeltaX, direction.DeltaY) * 16);
        tween.TweenProperty(entity, "global_position", newGlobalPos, 0.075f);
        tween.TweenProperty(entity, "global_position", currentGlobalPos, 0.075f);
        tween.Connect("finished", Callable.From(() => {
            _isTweening = false;
        }));
    }

    public void DoMoveTweenByPosition(DiabloEntity entity, Point newPos) {
        _isTweening = true;

        var tween = GetTree().CreateTween();
        
        var newGlobalPos = new Vector2(newPos.X, newPos.Y) * 32;
        
        // GD.Print($"Current: {entity.GlobalPosition}, New: {newGlobalPos}");
        
        tween.TweenProperty(entity, "global_position", newGlobalPos, 0.15f);
        tween.Connect("finished", Callable.From(() => {
            _isTweening = false;
        }));
    }

    public void MoveCameraToPoint(Point point) {
        _camera.GlobalPosition = new Vector2(point.X, point.Y) * 32;
    }
}
