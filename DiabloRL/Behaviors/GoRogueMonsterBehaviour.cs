using DiabloRL.Actions.Basic;
using DiabloRL.Entities;

namespace DiabloRL.Behaviors;

public class GoRogueMonsterBehaviour : GoRogueBehaviour
{
    protected Enemy Enemy => Parent as Enemy;
    
    public GoRogueMonsterBehaviour(int energyPerTurn, bool usesEnergy = true) : base(energyPerTurn, usesEnergy)
    {
        
    }

    public override void ProcessGameFrame()
    {
        base.ProcessGameFrame();

        var target = Game.GameScreen.Player;

        // check for distance to player
        // TODO: Have monster handle their own FOV
        
        // find a path to the player
        var path = Enemy.CurrentMap.AStar.ShortestPath(Enemy.Position, target.Position);
        
        // couldn't find a path so just stand around like an idiot
        if (path == null) return;
        
        // otherwise try to move to the next point on the path
        var firstPoint = path.GetStep(0);
        Game.GameScreen.ActionStack.Push(new GoRogueWalkAction(Enemy, firstPoint));
    }
}