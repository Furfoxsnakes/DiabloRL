using DiabloRL.Scripts.Cartography.Tiles;
using DiabloRL.Scripts.Components;
using Godot;
using GoRogue.GameFramework;
using GoRogue.MapGeneration;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;

namespace DiabloRL.Scripts.Common.States.DungeonStates;

[GlobalClass]
public partial class DungeonGenerateState : DungeonState {
    [Export] private GameObjectDetails _floorDetails;
    [Export] private GameObjectDetails _wallDetails;
    [Export] private GameObjectDetails _playerDetails;
    public override void Enter() {

        GD.Print("Generating dungeon...");
        GenerateTerrain();
        SpawnPlayer();
        
        Complete();
    }

    private void GenerateTerrain() {
        // var terrainMap = new ArrayMap2D<bool>(25, 25);
        // QuickGenerators.GenerateRectangleMap(terrainMap);

        var generator = new Generator(25, 25);
        generator.ConfigAndGenerateSafe(gen => {
            gen.AddSteps(DefaultAlgorithms.RectangleMapSteps());
        });

        var wallFloorValue = generator.Context.GetFirst<ISettableGridView<bool>>("WallFloor");
        foreach (var pos in wallFloorValue.Positions()) {
            DiabloGameObject tile = null;
            if (wallFloorValue[pos]) {
                tile = new DiabloGameObject(pos, _floorDetails, layer: 0);
            } else {
                tile = new DiabloGameObject(pos, _wallDetails, layer: 0);
            }

            Dungeon.Map = new Map(25, 25, 1, Distance.Chebyshev);
            Dungeon.Map.SetTerrain(tile);
            AddChild(tile);
        }
    }

    private void SpawnPlayer() {
        var player = new DiabloGameObject((5, 5), _playerDetails, 1);
        Dungeon.PlayerObject = player;
        Dungeon.Map.AddEntity(player);
        var playerInputComponent = new PlayerInputComponent();
        player.GoRogueComponents.Add(playerInputComponent);
        AddChild(player);
    }
}