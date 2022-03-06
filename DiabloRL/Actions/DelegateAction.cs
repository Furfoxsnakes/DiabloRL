using System;
using DiabloRL.Entities;
using SadRogue.Integration;

namespace DiabloRL.Actions
{
    /// <summary>
    /// Action that executes a given function
    /// </summary>
    public class DelegateAction : Action
    {
        public DelegateAction(GameEntity gameEntity) : base(gameEntity)
        {
        }

        protected void SetCallback(Func<ActionResult> callback) => _callBack = callback;

        protected override ActionResult OnProcess()
        {
            return _callBack();
        }

        private Func<ActionResult> _callBack;
    }
}