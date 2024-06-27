using DiabloRL.Scripts.Cartography.Tiles;
using DiabloRL.Scripts.Components;
using DiabloRL.Scripts.Interfaces;
using DiabloRL.Scripts.Processing;
using DiabloRL.Scripts.Processing.Actions;
using Godot;
using GoRogue.GameFramework;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Common.States.DungeonStates;

[GlobalClass]
public partial class DungeonExploreState : DungeonState {
    public override void Enter() {
        GD.Print("Exploring the dungeon...");
    }

    public override void Do(float delta) {
        var result = new GameResult();

        while (!result.NeedsAction) {
            result = Game.Instance.ProcessGame();
        }
    }

    public override void HandleInput(InputEvent inputEvent) {
        var movementComponent = Dungeon.PlayerEntity.GoRogueComponents.GetFirst<PlayerInputComponent>();
        var movement = movementComponent.GetMovement(inputEvent);
        Dungeon.PlayerEntity.SetNextAction(new WalkAction(Dungeon.PlayerEntity, movement));
    }
}