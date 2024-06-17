using Godot;
using GoRogue.FOV;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Cartography.Tiles;

public partial class DiabloEntity : DiabloGameObject {

    public DiabloEntity(Point pos, GameObjectDetails details) : base(pos, details, 1) {
        
    }
    
    public override void OnPlayerFovCalcuated(IFOV fov) {
        Visible = fov.BooleanResultView[Position];
    }
}