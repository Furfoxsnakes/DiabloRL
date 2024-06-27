using System.Threading.Tasks;
using DiabloRL.Scripts.Cartography.Dungeon;
using DiabloRL.Scripts.Cartography.Tiles;
using Godot;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Processing.Actions;

public partial class WalkAction : Action {

    private Direction _direction;
    // private Dungeon _dungeon;
    private bool _checkForCancel;
    
    public WalkAction(DiabloEntity entity, Direction direction, bool checkForCancel = false) : base(entity) {
        _direction = direction;
        // _dungeon = dungeon;
        _checkForCancel = checkForCancel;
    }

    protected override ActionResult OnProcess() {
        var newPos = DiabloEntity.Position + _direction;
        var tile = DiabloEntity.CurrentMap.GetObjectAt<DiabloGameObject>(newPos);

        // check for a door and open it if it's closed
        if (tile is Door door) {
            if (!door.IsOpen)
            {
                door.Open();
                return ActionResult.Done;
            }
        }

        // Check if there is an entity occupying the space
        var occupier = DiabloEntity.CurrentMap.GetEntityAt<DiabloEntity>(newPos);
        if (occupier != null && occupier != DiabloEntity) {
            Game.Instance.DoBumpTweenByDirection(DiabloEntity, Direction.GetDirection(DiabloEntity.Position, occupier.Position));
            return new AttackAction(DiabloEntity, occupier, DiabloEntity.GetAttack(occupier));
        }

        if (!DiabloEntity.CurrentMap.GameObjectCanMove(DiabloEntity, newPos)) {
            var direction = Direction.GetDirection(DiabloEntity.Position, newPos);
            if (direction != Direction.None) {
                Game.Instance.DoBumpTweenByDirection(DiabloEntity, direction);
            }
            
            return ActionResult.Done;
        }
        
        Game.Instance.DoMoveTweenByPosition(DiabloEntity,newPos);
        DiabloEntity.Position = newPos;

        return ActionResult.Done;
    }
}