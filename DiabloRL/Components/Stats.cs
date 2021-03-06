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

        public Stats()
        {
            _data = new int[(int) StatTypes.Count];
        }
        
        private void SetValue(StatTypes type, int value)
        {
            if (_data[(int) type] == value) return;
            
            _data[(int)type] = value;
        }
    }
}