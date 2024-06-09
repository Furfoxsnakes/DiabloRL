using DiabloRL.Scripts.Common.States;
using Godot;

namespace DiabloRL.Scripts.Common;

[GlobalClass]
public partial class GameStatemachine : Statemachine {
    protected Game Game => Owner as Game;
    public override Node Owner { get; set; }
}