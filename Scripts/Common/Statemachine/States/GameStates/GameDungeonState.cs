using Godot;

namespace DiabloRL.Scripts.Common.States.GameStates;

[GlobalClass]
public partial class GameDungeonState : GameState {
    public override void Enter() {
        GD.Print("Delving a dungeon...");
    }
}