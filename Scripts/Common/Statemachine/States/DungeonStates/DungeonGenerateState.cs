using DiabloRL.Scripts.Cartography.Tiles;
using DiabloRL.Scripts.Cartography.Tiles.Entities;
using DiabloRL.Scripts.Components;
using DiabloRL.Scripts.Processing.Behaviours;
using Godot;
using GoRogue.FOV;
using GoRogue.GameFramework;
using GoRogue.MapGeneration;
using GoRogue.MapGeneration.ContextComponents;
using GoRogue.MapGeneration.Steps;
using GoRogue.MapGeneration.Steps.Translation;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;

namespace DiabloRL.Scripts.Common.States.DungeonStates;

[GlobalClass]
public partial class DungeonGenerateState : DungeonState {
    [Export] private GameObjectDetails _floorDetails;
    [Export] private GameObjectDetails _wallDetails;
    [Export] private GameObjectDetails _playerDetails;
    [Export] private GameObjectDetails _doorDetails;

    [Export(PropertyHint.Range, "0, 50")] private int _numMonstersToSpawn = 10;
    [Export] private GameObjectDetails _skelemanDetails;

    private ItemList<Rectangle> _rooms;
    private DoorList _doors;
    
    public override void Enter() {

        GD.Print("Generating dungeon...");
        
        GenerateTerrain();
        SpawnPlayer();
        SpawnMonsters();
        
        Complete();
    }

    private void GenerateTerrain() {
        var generator = new Generator(50, 50);
        
        generator.ConfigAndGenerateSafe(gen => {
            gen.AddStep(
                
                new RoomsGeneration {
                    MinRooms = 10,
                    MaxRooms = 20,
                    RoomMinSize = 5,
                    RoomMaxSize = 9
                }
            )
            .AddStep(
                new MazeGeneration()
            ).AddStep(
                new RectanglesToAreas("Rooms", "Areas")
            ).AddStep(
                new RoomDoorConnection {
                    CancelConnectionPlacementChance = 90
                }
            ).AddStep(
                new TunnelDeadEndTrimming {
                    SaveDeadEndChance = 0
                }
            );
        });
        
        Dungeon.Map = new Map(generator.Context.Width, generator.Context.Height, 1, Distance.Chebyshev);
        // should be moved to a new class that inherits GameFramework.Map
        // Just doing it here for testing purposes
        var transparencyView = new LambdaTranslationGridView<IGameObject, bool>(
                Dungeon.Map.Terrain, t=> t.IsTransparent
            );
        Dungeon.Map.PlayerFOV = new RecursiveShadowcastingFOV(transparencyView); 

        var wallFloorValue = generator.Context.GetFirst<ISettableGridView<bool>>("WallFloor");
        foreach (var pos in wallFloorValue.Positions()) {
            DiabloTerrain tile = null;
            if (wallFloorValue[pos]) {
                tile = new DiabloTerrain(pos, _floorDetails);
            } else {
                tile = new DiabloTerrain(pos, _wallDetails);
            }
            
            Dungeon.Map.SetTerrain(tile);
            Dungeon.AddDiabloGameObject(tile);
        }

        _rooms = generator.Context.GetFirst<ItemList<Rectangle>>("Rooms");
        _doors = generator.Context.GetFirst<DoorList>("Doors");

        foreach (var rectangleDoors in _doors.DoorsPerRoom) {
            foreach (var doorPos in rectangleDoors.Value.Doors) {
                // var door = new DiabloGameObject(doorPos, _doorDetails, 0);
                var door = new Door(doorPos);
                Dungeon.Map.SetTerrain(door);
                Dungeon.AddDiabloGameObject(door);
            }
        }
    }

    private void SpawnMonsters() {

        foreach (var rectangle in _rooms.Items) {
            var randNumMonsters = GD.RandRange(1, 1);
            for (var i = 0; i < randNumMonsters; i++) {
                var randomPos = GetRandomPositionFromRectangle(rectangle);
                var skeleman = new Monster(randomPos, _skelemanDetails, Dungeon);
                while (!Dungeon.Map.CanAddEntity(skeleman)) {
                    skeleman.Position = GetRandomPositionFromMap();
                }
                
                Dungeon.AddEntity(skeleman);
                skeleman.Name = $"{_skelemanDetails.Name} {i}";
                // Dungeon.Map.AddEntity(skeleman);
                // Dungeon.AddDiabloGameObject(skeleman);
            }
        }
    }

    private void SpawnPlayer() {
        var firstRoom = _rooms.Items[0];
        var player = new Player(firstRoom.Center, _playerDetails);
        Dungeon.PlayerEntity = player;
        Dungeon.AddEntity(player);
        player.Name = "Player";
        // Dungeon.Map.AddEntity(player);
        // Dungeon.AddDiabloGameObject(player);
        Dungeon.CalculatePlayerFov();
    }

    private Point GetRandomPositionFromMap() {
        GD.Randomize();
        var randX = GD.RandRange(0, Dungeon.Map.Width);
        var randY = GD.RandRange(0, Dungeon.Map.Height);
        return (randX, randY);
    }

    private Point GetRandomPositionFromRectangle(Rectangle rect) {
        GD.Randomize();
        var randPosX = GD.RandRange(rect.MinExtentX, rect.MaxExtentX);
        var randPosY = GD.RandRange(rect.MinExtentY, rect.MaxExtentY);
        return (randPosX, randPosY);
    }
}