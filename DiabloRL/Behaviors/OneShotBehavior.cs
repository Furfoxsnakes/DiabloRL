using DiabloRL.Actions;
using SadConsole;

namespace DiabloRL.Behaviors
{
    public class OneShotBehavior : Behavior
    {
        public override bool NeedsUserInput => _action == null;

        public OneShotBehavior()
        {
            
        }
        
        public OneShotBehavior(Action action)
        {
            _action = action;
        }
        
        public override Action NextAction()
        {
            var action = _action;
            _action = null;
            return action;
        }

        private Action _action;
    }
}