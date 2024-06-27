using DiabloRL.Scripts.Components;
using DiabloRL.Scripts.Processing.Actions;
using DiabloRL.Scripts.Processing.Behaviours;
using Godot;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Cartography.Tiles.Entities;

public partial class Player : DiabloEntity {
    
    public Camera2D Camera { get; private set; }
    public Player(Point pos, GameObjectDetails details) : base(pos, details) {
        SetBehaviour(new OneShotBehaviour(new WalkAction(this, Direction.None)));
        GoRogueComponents.Add(new PlayerInputComponent());
        
        Energy.Fill();
        
        Game.Instance.MoveCameraToPoint(Position);
        
        // Camera = new Camera2D {
        //     Zoom = new Vector2(2, 2)
        // };
        // AddChild(Camera);
    }

    public override void OnPositionChanged(object sender, ValueChangedEventArgs<Point> e) {
        Game.Instance.MoveCameraToPoint(e.NewValue);
    }
}