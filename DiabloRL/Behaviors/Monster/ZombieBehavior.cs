using System;
using System.Linq;
using DiabloRL.Actions.Basic;
using DiabloRL.Components.Stats;
using DiabloRL.Entities;
using DiabloRL.Extensions;
using GoRogue.GameFramework;
using SadRogue.Primitives;
using Action = DiabloRL.Actions.Action;

namespace DiabloRL.Behaviors.Monster;

public class ZombieBehavior : MonsterBehavior
{
    
    public ZombieBehavior(Enemy enemy) : base(enemy)
    {
    }
    
    public override Action NextAction()
    {
        // doesn't do anything if not in the light radius (FOV) of the player
        if (!Enemy.CurrentMap.PlayerFOV.CurrentFOV.Contains(Enemy.Position))
            return new WalkAction(Enemy, Direction.None);
        
        var target = Game.GameScreen.Player;
        var distanceToTarget = (int)Distance.Chebyshev.Calculate(Enemy.Position, target.Position);

        var r = Rand.Next(100);                                             // generic random number
        var intf = Enemy.AllComponents.GetFirstOrDefault<Intelligence>().Current;   // intelligence factor
        
        // try to attack target if within range
        if (distanceToTarget == 1)
            if (r < (intf * 2) + 10)
                return new AttackAction(Enemy, target);
            else
                return new WalkAction(Enemy, Direction.None);

        // try to move towards target
        if (distanceToTarget >= 2 && distanceToTarget <= (intf * 2) + 3)
            if (r < (intf * 2) + 10)
            {
                var directionToTarget = Direction.GetDirection(Enemy.Position, target.Position);
                return new WalkAction(Enemy, directionToTarget);
            }
            else
            {
                return new WalkAction(Enemy, Direction.None);
            }

        // target is too far aawy so the dumb ass zombie doesn't know what to do and just walk around
        if (r >= (intf * 2) + 10)
            return new WalkAction(Enemy, Direction.None);
        
        r = Rand.Next(100);
        if (r < (intf * 2) + 20)
        {
            // move in a random direction if possible
            var randDirection = DirectionExtensions.RandomDirection();
            if (Enemy.CanMoveIn(randDirection))
                return new WalkAction(Enemy, randDirection);
            
            //stand around like an idiot if not
            return new WalkAction(Enemy, Direction.None);
        }
        
        // continue to move in the last moved direction
        return new WalkAction(Enemy, Enemy.PreviousMoveDirection);

    }

    
}