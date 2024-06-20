using DiabloRL.Scripts.Cartography.Dungeon;
using DiabloRL.Scripts.Cartography.Tiles;
using Godot;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Processing.Actions;

public partial class WalkAction : Action {

    private Direction _direction;
    private Dungeon _dungeon;
    private bool _checkForCancel;
    
    public WalkAction(DiabloEntity entity, Direction direction, Dungeon dungeon, bool checkForCancel = false) : base(entity) {
        _direction = direction;
        _dungeon = dungeon;
        _checkForCancel = checkForCancel;
    }

    protected override ActionResult OnProcess() {
        var newPos = DiabloEntity.Position + _direction;
        var tile = _dungeon.Map.GetObjectAt<DiabloGameObject>(newPos);

        // check for a door and open it if it's closed
        if (tile is Door door) {
            GD.Print($"{DiabloEntity.Details.Name} bumps his nose into a door. Ouch!");
            return ActionResult.Done;
        }

        // Check if there is an entity occupying the space
        var occupier = _dungeon.Map.GetEntityAt<DiabloEntity>(newPos);
        if (occupier != null && occupier != DiabloEntity) {
            GD.Print($"{DiabloEntity.Details.Name} attacks {occupier.Details.Name} with their fists");
            return ActionResult.Done;
        }

        if (!_dungeon.Map.GameObjectCanMove(DiabloEntity, newPos)) {
            GD.Print("Cannot walk there.");
            return ActionResult.Done;
        }

        DiabloEntity.Position = newPos;
        if (DiabloEntity == _dungeon.PlayerEntity) {
            _dungeon.CalculatePlayerFov();
        }
        
        return ActionResult.Done;
    }
}