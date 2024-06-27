using DiabloRL.Scripts.Cartography.Tiles;
using DiabloRL.Scripts.Cartography.Tiles.Entities;

namespace DiabloRL.Scripts.Processing.Actions.Moves;

public partial class BowMove : Move {
    
    public override string Description => "Fires an arrow from it's bow.";

    public override bool WillUseMove(Monster monster, DiabloEntity target) {
        return monster.CurrentMap.DistanceMeasurement.Calculate(monster.Position, target.Position) <= 3;
    }

    public override Action GetAction(Monster monster, DiabloEntity target) {
        return new AttackAction(monster, target, monster.GetAttack(target));
    }
}