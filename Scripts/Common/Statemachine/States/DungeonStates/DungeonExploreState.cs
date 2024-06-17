using DiabloRL.Scripts.Cartography.Tiles;
using DiabloRL.Scripts.Components;
using DiabloRL.Scripts.Interfaces;
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
        foreach (DiabloGameObject entity in Dungeon.Map.Entities.Items) {
            if (entity.GoRogueComponents.Contains<PlayerInputComponent>()) {
                var movement = entity.GoRogueComponents.GetFirst<PlayerInputComponent>().GetMovement(inputEvent);
                if (movement != Direction.None) {
                    // var newPos = entity.Position + movement;
                    if (entity.CanMoveIn(movement)) {
                        entity.Position += movement;
                        Dungeon.CalculatePlayerFov();
                    } else {
                        var blockingObject = Dungeon.Map.GetObjectAt<DiabloGameObject>(entity.Position + movement);
                        if (blockingObject is IBumpable bumpable) {
                            if (bumpable.OnBumped(entity)) {
                                Dungeon.CalculatePlayerFov();
                            }
                        }
                    }
                }
            }
        }
    }
}