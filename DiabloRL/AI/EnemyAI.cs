using System.Linq;
using SadRogue.Integration;
using SadRogue.Integration.Components;

namespace DiabloRL.AI
{
    /// <summary>
    /// Simple component that moves its parent toward the player if the player is visible. It demonstrates the basic
    /// usage of the integration library's component system, as well as basic AStar pathfinding.
    /// </summary>
    abstract class EnemyAI : RogueLikeComponentBase<RogueLikeEntity>
    {
        public EnemyAI()
            : base(false, false, false, false)
        {
        }

        public abstract void TakeTurn();
    }
}