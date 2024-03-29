﻿using System;
using DiabloRL.Actions.Basic;
using DiabloRL.Entities;
using SadRogue.Primitives;
using Action = DiabloRL.Actions.Action;

namespace DiabloRL.Behaviors
{
    public class MonsterBehavior : Behavior
    {
        public Enemy Enemy => _enemy;
        protected Random Rand;
        
        public MonsterBehavior(Enemy enemy)
        {
            _enemy = enemy;
            Rand = new Random(System.DateTime.UtcNow.Second);
        }

        public override Action NextAction()
        {
            var target = Game.GameScreen.Player;
            var distance = target.Position - _enemy.Position;
            
            var path = _enemy.CurrentMap.AStar.ShortestPath(_enemy.Position, target.Position);
            
            // couldn't find a path so the monster will just stand still like an idiot
            if (path == null) return new WalkAction(_enemy, Direction.None);
            
            // otherwise try to move to next point on path
            var firstPoint = path.GetStep(0);
            var directionToNextPoint = Direction.GetDirection(_enemy.Position, firstPoint);
            return new WalkAction(_enemy, directionToNextPoint);
        }

        private Enemy _enemy;
    }
}