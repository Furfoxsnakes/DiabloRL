using DiabloRL.Actions;
using DiabloRL.Entities;
using SadRogue.Integration;
using SadRogue.Integration.Components;

namespace DiabloRL.Behaviors
{
    public abstract class Behavior
    {
        public virtual bool NeedsUserInput => false;
        
        public abstract Action NextAction();
        
        /// <summary>
        /// Called when another action disturbs the entity, such as being hit
        /// </summary>
        public virtual void Disturb(){}
        
        /// <summary>
        /// Called when the user indicates they want to cancel this behavior
        /// </summary>
        public virtual void Cancel(){}
    }
}