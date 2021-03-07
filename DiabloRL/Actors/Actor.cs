using DiabloRL.Components;
using DiabloRL.Enums;
using GoRogue;
using Microsoft.Xna.Framework;
using SadConsole;

namespace DiabloRL.Actors
{
    public class Actor : BasicEntity
    {
        public string Name;
        
        public Actor(Color foreground, Color background, int glyph, string name, Coord position, int layer, bool isWalkable, bool isTransparent) : base(foreground, background, glyph, position, layer, isWalkable, isTransparent)
        {
            Name = name;
        }

        public virtual void TakeDamage(int amount)
        {
            // handled by inheriting class
        }

        public virtual void Attack(Actor defender)
        {
            if (ResolveToHit(defender))
            {
                defender.TakeDamage(ResolveDamage(defender));
            }
            else
            {
                System.Console.WriteLine($"{Name} missed {defender.Name}");
            }
        }

        protected virtual void Die()
        {
            System.Console.WriteLine($"{Name} has been slain");
        }

        protected virtual bool ResolveToHit(Actor defender)
        {
            return true;
        }

        protected virtual int ResolveDamage(Actor defender)
        {
            return 0;
        }
    }
}