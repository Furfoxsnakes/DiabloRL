using System;
using DiabloRL.Actors;
using DiabloRL.Enums;
using SadConsole.Components.GoRogue;

namespace DiabloRL.Components
{
    public class MonsterStats : ComponentBase<Monster>
    {
        public int this[MonsterStatTypes type]
        {
            get => _data[(int) type];
            set => SetValue(type, value);
        }
        
        private int[] _data;

        public MonsterStats()
        {
            _data = new int[(int) MonsterStatTypes.Count];
        }
        
        private void SetValue(MonsterStatTypes type, int value)
        {
            if (_data[(int) type] == value) return;
            
            _data[(int)type] = value;
        }
    }
}