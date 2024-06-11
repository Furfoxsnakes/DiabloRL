using System;
using System.Collections.Generic;
using Godot;
using GoRogue;
using GoRogue.Components;
using GoRogue.GameFramework;
using GoRogue.Random;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Cartography.Tiles;

public partial class DiabloGameObject : Sprite2D, IGameObject {
    private GameObjectDetails _gameObjectDetails;

    public DiabloGameObject(Point pos, GameObjectDetails details, int layer) : this(pos, layer, details.IsWalkable, details.IsTransparent) {
        _gameObjectDetails = details;
        Position = pos;
        Texture = _gameObjectDetails.SpriteTexture;
        GD.Print(IsWalkable);
    }
    public DiabloGameObject() {
        
    }
    private bool _isWalkable;
    private Point _position;
    
    public DiabloGameObject(Point position, int layer, bool isWalkable = true, bool isTransparent = true,
                      Func<uint>? idGenerator = null, IComponentCollection? customComponentCollection = null)
        : this(layer, isWalkable, isTransparent, idGenerator, customComponentCollection)
    {
        Position = position;
    }

    public DiabloGameObject(int layer, bool isWalkable = true, bool isTransparent = true,
                      Func<uint>? idGenerator = null, IComponentCollection? customComponentCollection = null)
    {
        idGenerator ??= GlobalRandom.DefaultRNG.NextUInt;

        Layer = layer;
        IsWalkable = isWalkable;
        IsTransparent = isTransparent;

        _currentMap = null;

        ID = idGenerator();
        GoRogueComponents = customComponentCollection ?? new ComponentCollection();
        GoRogueComponents.ParentForAddedComponents = this;
    }

    /// <inheritdoc />
    public new Point Position
    {
        get => _position;
        set {
            this.SafelySetProperty( ref _position, value, PositionChanging, PositionChanged);
            GlobalPosition = new Vector2(value.X, value.Y) * 32;
        }
    }

    /// <inheritdoc />
    public event EventHandler<ValueChangedEventArgs<Point>>? PositionChanging;

    /// <inheritdoc />
    public event EventHandler<ValueChangedEventArgs<Point>>? PositionChanged;

    /// <inheritdoc />
    public bool IsWalkable
    {
        get => _isWalkable;
        set => this.SafelySetProperty(ref _isWalkable, value, WalkabilityChanging, WalkabilityChanged);
    }

    /// <inheritdoc />
    public event EventHandler<ValueChangedEventArgs<bool>>? WalkabilityChanging;

    /// <inheritdoc />
    public event EventHandler<ValueChangedEventArgs<bool>>? WalkabilityChanged;

    private bool _isTransparent;

    /// <inheritdoc />
    public bool IsTransparent
    {
        get => _isTransparent;
        set => this.SafelySetProperty(ref _isTransparent, value, TransparencyChanging, TransparencyChanged);
    }

    /// <inheritdoc />
    public event EventHandler<ValueChangedEventArgs<bool>>? TransparencyChanging;

    /// <inheritdoc />
    public event EventHandler<ValueChangedEventArgs<bool>>? TransparencyChanged;

    /// <inheritdoc />
    public uint ID { get; }

    /// <inheritdoc />
    public int Layer { get; }

    private Map? _currentMap;

    /// <inheritdoc />
    public Map? CurrentMap => _currentMap;

    /// <inheritdoc />
    public event EventHandler<GameObjectCurrentMapChanged>? AddedToMap;

    /// <inheritdoc />
    public event EventHandler<GameObjectCurrentMapChanged>? RemovedFromMap;

    /// <inheritdoc />
    public IComponentCollection GoRogueComponents { get; }

    /// <inheritdoc />
    public void OnMapChanged(Map? newMap)
        => this.SafelySetCurrentMap(ref _currentMap, newMap, AddedToMap, RemovedFromMap);
}