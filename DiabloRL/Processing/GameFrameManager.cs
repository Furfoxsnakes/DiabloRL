using System;
using System.Linq;
using DiabloRL.Entities;
using SadConsole;
using SadConsole.Components;
using SadConsole.Components.GoRogue;

namespace DiabloRL.Processing;

public class GameFrameManager : UpdateComponent
{
    public bool RunLogicFrame;
    public event EventHandler LogicFrameCompleted;
    public GameMap Map { get; }
    public MapScreen MapScreen { get; }

    public GameFrameManager(GameMap map, MapScreen mapScreen)
    {
        Map = map;
        MapScreen = mapScreen;
    }
    
    public override void Update(IScreenObject host, TimeSpan delta)
    {
        if (RunLogicFrame)
        {
            foreach (GameEntity entity in Map.Entities.Items.Where(e => e.GoRogueComponents.Contains<IGameFrameProcessor>()))
            {
                if (entity is not Player)
                {
                    foreach (var processor in entity.AllComponents.GetAll<IGameFrameProcessor>())
                    {
                        processor.ProcessGameFrame();
                        if (!RunLogicFrame)
                        {
                            LogicFrameCompleted?.Invoke(this, EventArgs.Empty);
                            return;
                        }
                    }
                }
            }
            
            // process the player
            foreach (var processor in MapScreen.Player.AllComponents.GetAll<IGameFrameProcessor>())
            {
                processor.ProcessGameFrame();
                if (!RunLogicFrame)
                {
                    LogicFrameCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }

            RunLogicFrame = false;
            
            LogicFrameCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}