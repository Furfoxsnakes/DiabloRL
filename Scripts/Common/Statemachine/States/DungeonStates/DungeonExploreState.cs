using System.Linq;
using DiabloRL.Scripts.Cartography.Dungeon;
using DiabloRL.Scripts.Cartography.Tiles;
using DiabloRL.Scripts.Components;
using Godot;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Common.States.DungeonStates;

[GlobalClass]
public partial class DungeonExploreState : DungeonState {
    public override void Enter() {
        GD.Print("Exploring the dungeon...");
    }

    public override void HandleInput(InputEvent inputEvent) {
        var entity = Dungeon.Map.GetEntityAt<DiabloGameObject>((5, 5));
        entity.Position += (1, 0);
        // foreach (var entity in Dungeon.Map.Entities.Items) {
        //     if (entity == Dungeon.PlayerObject) {
        //         var movement = entity.GoRogueComponents.GetFirst<PlayerInputComponent>().GetMovement(inputEvent);
        //         if (movement != Point.Zero) {
        //             entity.Position += movement;
        //         }
        //     }
        // }
    }
}