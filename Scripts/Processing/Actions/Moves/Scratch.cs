using DiabloRL.Scripts.Cartography.Tiles;
using DiabloRL.Scripts.Cartography.Tiles.Entities;
using DiabloRL.Scripts.Processing.Things;
using Godot;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Processing.Actions.Moves;

public partial class Scratch : Move {
    public override string Description => "It scratches";

    public override bool WillUseMove(Monster monster, DiabloEntity target) {
        var distance = monster.CurrentMap.DistanceMeasurement.Calculate(monster.Position, target.Position);
        return distance <= 1;
    }

    public override Action GetAction(Monster monster, DiabloEntity target) {
        var attack = monster.GetAttack(target);
        return new MeleeAttackAction(monster, target, attack);
    }
}