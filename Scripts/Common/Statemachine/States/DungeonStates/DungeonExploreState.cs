using System.Linq;
using DiabloRL.Scripts.Cartography.Dungeon;
using DiabloRL.Scripts.Cartography.Tiles;
using DiabloRL.Scripts.Components;
using Godot;
using GoRogue.GameFramework;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Common.States.DungeonStates;

[GlobalClass]
public partial class DungeonExploreState : DungeonState {
    public override void Enter() {
        GD.Print("Exploring the dungeon...");
    }

    public override void HandleInput(InputEvent inputEvent) {
        foreach (var entity in Dungeon.Map.Entities.Items) {
            if (entity.GoRogueComponents.Contains<PlayerInputComponent>()) {
                var movement = entity.GoRogueComponents.GetFirst<PlayerInputComponent>().GetMovement(inputEvent);
                if (movement != Point.Zero) {
                    if (entity.CanMove(entity.Position + movement)) {
                        entity.Position += movement;
                    }
                }
            }
        }
    }
}