using DiabloRL.Scripts.Cartography.Dungeon;
using Godot;

namespace DiabloRL.Scripts.Common;

[GlobalClass]
public partial class DungeonStateMachine : Statemachine {
    public override Node Owner { get; set; }
    public Dungeon Dungeon => Owner as Dungeon;
}