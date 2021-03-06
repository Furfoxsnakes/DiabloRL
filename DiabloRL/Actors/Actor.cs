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

        public void TakeDamage(int amount)
        {
            var stats = GetGoRogueComponent<Stats>();

            if (stats == null)
            {
                System.Console.WriteLine($"{Name} does not have a stats component and therefore cannot take damage.");
                return;
            }
            
            System.Console.WriteLine($"{Name} takes {amount} point(s) of damage.");

            stats[StatTypes.LIFE] -= amount;

            if (stats[StatTypes.LIFE] <= 0)
            {
                Die();
                return;
            }

            System.Console.WriteLine($"{Name} has {stats[StatTypes.LIFE]} life remaining");
        }

        private void Die()
        {
            System.Console.WriteLine($"{Name} has been slain");
            
            if (this is Monster monster)
                (CurrentMap as DungeonMap)?.RemoveMonster(monster);

            if (this is Player)
            {
                // End the game
                // TODO: Create an end game screen
            }
        }
    }
}