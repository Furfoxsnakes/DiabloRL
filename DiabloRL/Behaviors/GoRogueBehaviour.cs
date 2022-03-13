using DiabloRL.Entities;
using SadConsole.Components.GoRogue;
using SadRogue.Integration.Components;

namespace DiabloRL.Behaviors;

public abstract class GoRogueBehaviour : RogueLikeComponentBase<GameEntity>, IGameFrameProcessor
{
    protected int EnergyPerTurn;
    protected int CurrentEnergy;
    protected bool UsesEnergy;
    
    protected GoRogueBehaviour(int energyPerTurn, bool usesEnergy = true, uint sortOrder = 5) : base(isUpdate:false, isRender: false, isMouse:false, isKeyboard:false, sortOrder)
    {
        EnergyPerTurn = energyPerTurn;
        UsesEnergy = usesEnergy;
        CurrentEnergy = 0;
    }
    
    public virtual void ProcessGameFrame()
    {
        if (UsesEnergy)
        {
            CurrentEnergy += EnergyPerTurn;
            if (CurrentEnergy < 60) return;
        }
    }
}