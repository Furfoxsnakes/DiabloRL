using System;
using System.Collections.Generic;
using DiabloRL.Behaviors;
using DiabloRL.Components.Stats;
using GoRogue.Components;
using SadRogue.Integration;
using SadRogue.Primitives;
using Action = DiabloRL.Actions.Action;

namespace DiabloRL.Entities
{
    public class GameEntity : RogueLikeEntity
    {

        public Behavior Behavior => _behavior;
        
        public Life Life => AllComponents.GetFirstOrDefault<Life>();
        
        public GameEntity(Color foreground, Color background, int glyph, bool walkable = true, bool transparent = true, int layer = 1, Func<uint>? idGenerator = null, IComponentCollection? customComponentCollection = null) : base(foreground, background, glyph, walkable, transparent, layer, idGenerator, customComponentCollection)
        {
            
        }

        public IEnumerable<Action> TakeTurn()
        {
            var turnAction = _behavior.NextAction();

            yield return turnAction;
        }

        public void SetBehavior(Behavior behavior)
        {
            _behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
        }

        public virtual void TakeHit(int damage)
        {
            Life.Current -= damage;
        }

        private Behavior _behavior;
    }
}