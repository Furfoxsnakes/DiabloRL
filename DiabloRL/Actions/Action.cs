using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DiabloRL.Entities;
using DiabloRL.Things;
using SadRogue.Integration;

namespace DiabloRL.Actions
{
    public abstract class Action
    {
        /// <summary>
        /// Wraps the Action in an ActionResult. Allows returning an alternate action from Action's process method
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static implicit operator ActionResult(Action action) => new ActionResult(action);

        public GameEntity GameEntity => _gameEntity;
        public bool ConsumesEnergy => _consumesEnergy;
        public Queue<Action> Actions => _actions;
        public GameMap Map => _gameEntity.CurrentMap as GameMap;
        public MapScreen MapScreen => Game.GameScreen;

        public Action(GameEntity gameEntity)
        {
            if (gameEntity == null)
                throw new ArgumentNullException(nameof(gameEntity));

            _gameEntity = gameEntity;
        }

        // TODO: Implement energy at a future time
        public void MarkAsConsumesEnergy() => _consumesEnergy = true;

        public void AddAction(Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            _actions.Enqueue(action);
        }
        
        public void Log(string message)
        {
            MapScreen.MessageLog.AddMessage(message);
        }

        public ActionResult Fail(string message)
        {
            Log(message);
            return ActionResult.Fail;
        }

        public ActionResult Process(Queue<Action> actions)
        {
            _actions = actions;
            var result = OnProcess();
            _actions = null;

            return result;
        }

        public void AfterSuccess()
        {
            if (_consumesEnergy && (GameEntity != null))
            {
                GameEntity.AllComponents.GetFirstOrDefault<Energy>()?.Spend();
                _consumesEnergy = false; 
            }
        }

        protected abstract ActionResult OnProcess();
        
        private GameEntity _gameEntity;
        private bool _consumesEnergy;
        private Queue<Action> _actions;
    }
}