using Godot;

namespace DiabloRL.Scripts.Cartography.Tiles;

[GlobalClass]
public partial class GameObjectDetails : Resource {
    [Export] public string Name;
    
    [ExportCategory("Tile details")]
    [Export] public Texture2D SpriteTexture;
    [Export] public Texture2D AlternateSpriteTexture;
    [Export] public bool IsTransparent;
    [Export] public bool IsWalkable;

    [ExportCategory("Combat details")] 
    [Export] public int StartingLife;
    [Export] public int StartingMana;
    [Export] public int BaseStrength;
    [Export] public int BaseMagic;
    [Export] public int BaseDexterity;
    [Export] public int BaseVitality;
    [Export(PropertyHint.Range, "0,12,1")] public int Speed = 6;
}