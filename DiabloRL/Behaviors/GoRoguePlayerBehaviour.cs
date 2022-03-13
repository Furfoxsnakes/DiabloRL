namespace DiabloRL.Behaviors;

public class GoRoguePlayerBehaviour : GoRogueBehaviour
{
    public bool NeedsInput => CurrentEnergy >= 60;
    
    public GoRoguePlayerBehaviour(int energyPerTurn, bool usesEnergy = true) : base(energyPerTurn, usesEnergy)
    {
        
    }
}