using GoRogue.FOV;
using SadRogue.Primitives;
using Color = Godot.Color;

namespace DiabloRL.Scripts.Cartography.Tiles;

public partial class DiabloTerrain : DiabloGameObject {

    public DiabloTerrain(Point pos, GameObjectDetails details) : base(pos, details, 0) {
        Visible = false;
    }

    public DiabloTerrain(Point pos, bool isWalkable, bool isTransparent) : base(pos, 0, isWalkable, isTransparent) {
        Visible = false;
    }
    
    public override void OnPlayerFovCalcuated(IFOV fov) {
        Modulate = fov.BooleanResultView[Position] ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0.25f);
        
        if (!fov.BooleanResultView[Position]) return;
        Visible = true;
        IsExplored = true;
    }
}