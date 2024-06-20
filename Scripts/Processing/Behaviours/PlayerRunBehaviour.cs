using DiabloRL.Scripts.Cartography.Dungeon;
using DiabloRL.Scripts.Cartography.Tiles;
using DiabloRL.Scripts.Processing.Actions;
using GoRogue.GameFramework;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Processing.Behaviours;

public partial class PlayerRunBehaviour : PlayerBehaviour {
    private Direction _direction;
    private bool _isLeftOpen;
    private bool _isRightOpen;
    private Dungeon _dungeon;

    // need user input if the hero has stopped
    public override bool NeedsUserInput => _direction == Direction.None;

    public PlayerRunBehaviour(DiabloEntity playerEntity, Direction direction, Dungeon dungeon) : base(playerEntity) {
        _dungeon = dungeon;
        
        if (!PlayerEntity.CurrentMap.GameObjectCanMove(PlayerEntity, PlayerEntity.Position + _direction)) {
            direction = Direction.None;
        }

        _direction = direction;
        
        _isLeftOpen = PlayerEntity.CanMoveIn(Direction.Left);
        _isRightOpen = PlayerEntity.CanMoveIn(Direction.Right);
    }

    public override Action NextAction() {
        return new WalkAction(PlayerEntity, _direction, _dungeon, true);
    }
}