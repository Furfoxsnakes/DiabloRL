using System;
using System.Collections.Generic;
using Godot;
using GoRogue;
using GoRogue.Components;
using GoRogue.GameFramework;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Cartography.Tiles;

public partial class DiabloGameObject : Sprite2D, IGameObject {
    private GameObjectDetails _gameObjectDetails;
    private IGameObject _gameObjectImplementation;

    public DiabloGameObject(Point pos, GameObjectDetails details, int layer) {
        _gameObjectImplementation = new GameObject(pos, layer, details.IsWalkable, details.IsTransparent);
        Position = pos;
        _gameObjectDetails = details;
        Texture = _gameObjectDetails.SpriteTexture;
    }

    public DiabloGameObject() {
        
    }

    public uint ID => _gameObjectImplementation.ID;

    public int Layer => _gameObjectImplementation.Layer;

    public IComponentCollection GoRogueComponents => _gameObjectImplementation.GoRogueComponents;

    public new Point Position {
        get => _gameObjectImplementation.Position;
        set {
            _gameObjectImplementation.Position = value;
            GlobalPosition = new Vector2(value.X, value.Y) * 32;
        }
    }

    public event EventHandler<ValueChangedEventArgs<Point>> PositionChanging {
        add => _gameObjectImplementation.PositionChanging += value;
        remove => _gameObjectImplementation.PositionChanging -= value;
    }

    public event EventHandler<ValueChangedEventArgs<Point>> PositionChanged {
        add => _gameObjectImplementation.PositionChanged += value;
        remove => _gameObjectImplementation.PositionChanged -= value;
    }

    public void OnMapChanged(Map newMap) {
        _gameObjectImplementation.OnMapChanged(newMap);
    }

    public Map CurrentMap => _gameObjectImplementation.CurrentMap;

    public bool IsTransparent {
        get => _gameObjectImplementation.IsTransparent;
        set => _gameObjectImplementation.IsTransparent = value;
    }

    public bool IsWalkable {
        get => _gameObjectImplementation.IsWalkable;
        set => _gameObjectImplementation.IsWalkable = value;
    }

    public event EventHandler<GameObjectCurrentMapChanged> AddedToMap {
        add => _gameObjectImplementation.AddedToMap += value;
        remove => _gameObjectImplementation.AddedToMap -= value;
    }

    public event EventHandler<GameObjectCurrentMapChanged> RemovedFromMap {
        add => _gameObjectImplementation.RemovedFromMap += value;
        remove => _gameObjectImplementation.RemovedFromMap -= value;
    }

    public event EventHandler<ValueChangedEventArgs<bool>> TransparencyChanging {
        add => _gameObjectImplementation.TransparencyChanging += value;
        remove => _gameObjectImplementation.TransparencyChanging -= value;
    }

    public event EventHandler<ValueChangedEventArgs<bool>> TransparencyChanged {
        add => _gameObjectImplementation.TransparencyChanged += value;
        remove => _gameObjectImplementation.TransparencyChanged -= value;
    }

    public event EventHandler<ValueChangedEventArgs<bool>> WalkabilityChanging {
        add => _gameObjectImplementation.WalkabilityChanging += value;
        remove => _gameObjectImplementation.WalkabilityChanging -= value;
    }

    public event EventHandler<ValueChangedEventArgs<bool>> WalkabilityChanged {
        add => _gameObjectImplementation.WalkabilityChanged += value;
        remove => _gameObjectImplementation.WalkabilityChanged -= value;
    }
}