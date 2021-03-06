using System;
using DiabloRL.Actors;
using DiabloRL.Enums;
using SadConsole.Components.GoRogue;

namespace DiabloRL.Components
{
    [Serializable]
    public class Stats : ComponentBase<Actor>
    {
        public int this[StatTypes type]
        {
            get => _data[(int) type];
            set => SetValue(type, value);
        }
        
        private int[] _data;

        public Stats(int strength, int magic, int dexterity, int vitality, int life, int mana)
        {
            _data = new int[(int) StatTypes.Count];
            this[StatTypes.STRENGTH] = strength;
            this[StatTypes.MAGIC] = magic;
            this[StatTypes.DEXTERITY] = dexterity;
            this[StatTypes.VITALITY] = vitality;
            this[StatTypes.LIFE] = life;
            this[StatTypes.MANA] = mana;
        }
        
        private void SetValue(StatTypes type, int value)
        {
            if (_data[(int) type] == value) return;
            
            _data[(int)type] = value;
        }
    }
}