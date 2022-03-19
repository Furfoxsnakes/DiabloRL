using System;
using System.Collections.Generic;
using DiabloRL.Behaviors;
using DiabloRL.Components.Stats;
using DiabloRL.Things;
using GoRogue.Components;
using SadRogue.Integration;
using SadRogue.Primitives;
using Action = DiabloRL.Actions.Action;

namespace DiabloRL.Entities
{
    public abstract class GameEntity : RogueLikeEntity
    {

        public Behavior Behavior => _behavior;
        
        public Life Life => AllComponents.GetFirstOrDefault<Life>();

        public Direction PreviousMoveDirection;
        
        public GameEntity(Color foreground, Color background, int glyph, bool walkable = true, bool transparent = true, int layer = 1, Func<uint>? idGenerator = null, IComponentCollection? customComponentCollection = null) : base(foreground, background, glyph, walkable, transparent, layer, idGenerator, customComponentCollection)
        {
            
        }

        public IEnumerable<Action> TakeTurn()
        {
            var turnAction = _behavior.NextAction();
            turnAction.MarkAsConsumesEnergy();

            yield return turnAction;
        }

        public void SetBehavior(Behavior behavior)
        {
            _behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
        }

        public virtual void TakeHit(Action action, Hit hit)
        {
            // attempt to dodge the attack
            
            // apply the damage of the hit
            hit.SetDamage(ReceiveDamage(hit.Attack));

            if (hit.Damage > 0)
            {
                action.Log($"{hit.Attacker.Name} hits {Name} for {hit.Damage} damage.");

                if (Life.Current <= 0)
                {
                    action.Log($"{Name} has been slain by {hit.Attacker.Name}");
                    if (this is Enemy enemy)
                        Game.GameScreen.Player.Killed(action, enemy);
                    
                    Die(action);
                }
            }
            else
            {
                // tell why no damage was done
            }
        }

        public virtual int ReceiveDamage(Attack attack)
        {
            float amount = attack.Roll();

            // apply modifiers
            var appliedDamage = (int)Math.Ceiling(amount);
            Life.Current -= appliedDamage;
            return appliedDamage;
        }

        public abstract Attack GetAttack(GameEntity defender);

        public abstract void Die(Action action);

        private Behavior _behavior;
    }
}