using DiabloRL.Scripts.Cartography.Dungeon;
using DiabloRL.Scripts.Cartography.Tiles.Entities;
using DiabloRL.Scripts.Processing.Actions;
using DiabloRL.Scripts.Processing.Actions.Moves;
using Godot;
using GoRogue.GameFramework;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Processing.Behaviours;

public partial class MonsterBehaviour : Behaviour {

    private Dungeon _dungeon;
    private Monster _monster;

    public MonsterBehaviour(Monster monster, Dungeon dungeon) {
        _monster = monster;
        _dungeon = dungeon;
    }
    public override Action NextAction() {
        var target = _dungeon.PlayerEntity;
        var distance =_dungeon.Map.DistanceMeasurement.Calculate(_monster.Position, target.Position);

        // look to see if the player is in the monster's FOV
        _monster.FOV.Calculate(_monster.Position, 5);

        var playerIsInFOV = false;
        
        foreach (var pos in _monster.FOV.CurrentFOV) {
            if (pos == target.Position) {
                // player is in FOV
                playerIsInFOV = true;
                break;
            }
        }

        if (!playerIsInFOV) return new WalkAction(_monster, Direction.None);
        
        var move = _monster.GoRogueComponents.GetFirst<Move>();
        if (move.WillUseMove(_monster, target)) {
            return move.GetAction(_monster, target);
        }
        
        var path = _dungeon.Map.AStar.ShortestPath(_monster.Position, target.Position);
        if (path == null) return new WalkAction(_monster, Direction.None);
        
        var nextStep = path.GetStep(0);
        var direction = Direction.GetDirection(_monster.Position, nextStep);
        return new WalkAction(_monster, direction);
    }
}