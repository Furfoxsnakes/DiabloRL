using Godot;

namespace DiabloRL.Scripts.Cartography.Tiles;

[GlobalClass]
public partial class GameObjectDetails : Resource {
    [Export] public string Name;
    [Export] public Texture2D SpriteTexture;
    [Export] public bool IsTransparent;
    [Export] public bool IsWalkable;
}