using DiabloRL.Actors;
using SadConsole.Components.GoRogue;

namespace DiabloRL.Components
{
    public class Stats : ComponentBase<Actor>
    {
        public int Strength { get; set; }
        public int Magic { get; set; }
        public int Dexterity { get; set; }
        public int Vitality { get; set; }
        public int Life { get; set; }
        public int Mana { get; set; }

        public Stats(int strength, int magic, int dexterity, int vitality, int life, int mana)
        {
            Strength = strength;
            Magic = magic;
            Dexterity = dexterity;
            Vitality = vitality;
            Life = life;
            Mana = mana;
        }
    }
}