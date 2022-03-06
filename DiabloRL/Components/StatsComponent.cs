using SadRogue.Integration;
using SadRogue.Integration.Components;

namespace DiabloRL.Components
{
    public abstract class StatsComponent : RogueLikeComponentBase<RogueLikeEntity>
    {
        public int Health;
        public int MaxHealth;
        
        public StatsComponent() : base(isUpdate: false, isRender: false, isMouse: false, isKeyboard: false)
        {
            
        }

        public abstract int TakeDamage(int amount);
    }
}