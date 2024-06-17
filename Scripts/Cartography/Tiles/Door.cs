using DiabloRL.Scripts.Interfaces;
using Godot;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Cartography.Tiles;

public partial class Door : DiabloTerrain, IBumpable{
    public bool IsOpen { get; private set; }
    
    public Door(Point pos, bool isOpen = false) : base(pos, isWalkable: isOpen, isTransparent: isOpen) {
        IsOpen = isOpen;
        Details = GD.Load<GameObjectDetails>("res://Resources/Tiles/DoorTile.tres");
        Texture = Details.SpriteTexture;
    }

    public bool OnBumped(DiabloGameObject source) {
        if (IsOpen) {
            Close();
        } else {
            GD.Print($"{source.Details.Name} opens the door");
            Open();
        }
        return true;
    }

    public void Open() {
        IsOpen = true;
        IsWalkable = true;
        IsTransparent = true;
        Texture = Details.AlternateSpriteTexture;
    }

    public void Close() {
        IsOpen = false;
        IsWalkable = false;
        IsTransparent = false;
        Texture = Details.SpriteTexture;
    }
}