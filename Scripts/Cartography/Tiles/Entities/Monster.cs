using DiabloRL.Scripts.Processing.Actions.Moves;
using DiabloRL.Scripts.Processing.Behaviours;
using GoRogue.FOV;
using GoRogue.GameFramework;
using GoRogue.Pathing;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Cartography.Tiles.Entities;

public partial class Monster : DiabloEntity {

    public AStar Pathfinding { get; private set; }
    public FOVBase FOV { get; private set; }

    private Dungeon.Dungeon _dungeon;
    
    public Monster(Point pos, GameObjectDetails details, Dungeon.Dungeon dungeon) : base(pos, details) {
        _dungeon = dungeon;
        SetBehaviour(new MonsterBehaviour(this, dungeon));
        FOV = new RecursiveShadowcastingBooleanBasedFOV(dungeon.Map.TransparencyView);
        GoRogueComponents.Add(new Scratch());
        // GoRogueComponents.Add(new BowMove());
    }

    public override void Die() {
        base.Die();
        Game.Instance.Dungeon.RemoveEntity(this);
    }
}