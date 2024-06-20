using Godot;
using System;
using System.Collections.Generic;
using DiabloRL.Scripts.Cartography.Dungeon;
using DiabloRL.Scripts.Cartography.Tiles;
using DiabloRL.Scripts.Processing;
using Action = DiabloRL.Scripts.Processing.Action;

public partial class Game : Node {
    [Export] private Dungeon _dungeon;

    private static Game _instance;
    public static Game Instance => _instance;
    
    private IEnumerator<GameResult> _processEnumerator;

    private DiabloEntity _actingEntity;
    public DiabloEntity ActingEntity => _actingEntity;

    private DiabloEntity _cancelEntity;
    public DiabloEntity CancelEntity => _cancelEntity;

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
            foreach (var gameObject in _dungeon.Map.Entities.Items) {
                if (gameObject is DiabloEntity entity) {
                    while (entity.Behaviour.NeedsUserInput) {
                        yield return new GameResult(GameResultFlags.NeedsUserInput, entity);
                    }

                    foreach (var action in entity.TakeTurn()) {
                        foreach (var result in ProcessAction(action)) {
                            yield return result;
                        }
                    }
                }
            }
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
            
            // TODO: after success functions

            _actingEntity = null;

            if (result.NeedsPause) {
                GD.Print("Waiting for input");
                yield return new GameResult(GameResultFlags.NeedsPause);
            } else if (result.NeedsCheckForCancel) {
                _cancelEntity = action.DiabloEntity;
                yield return new GameResult(GameResultFlags.CheckForCancel);
            }
        }
    }
}
