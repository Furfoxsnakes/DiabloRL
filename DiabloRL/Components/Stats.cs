using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Wrapper for LIFE stat to not allow it to be more than the MAX_LIFE stat
        /// </summary>
        public int Life
        {
            get => _data[(int) StatTypes.LIFE];
            set => _data[(int) StatTypes.LIFE] = Math.Min(Life + value, _data[(int) StatTypes.MAX_LIFE]);
        }
        
        /// <summary>
        /// Wrapper for MANA stat to not allow it to be more than the MAX_MANA stat
        /// </summary>
        public int Mana
        {
            get => _data[(int) StatTypes.MANA];
            set => _data[(int) StatTypes.MANA] = Math.Min(Mana + value, _data[(int) StatTypes.MAX_MANA]);
        }

        private static Dictionary<StatTypes, string> _didChangeNotifications = new Dictionary<StatTypes, string>();

        public Stats()
        {
            _data = new int[(int) StatTypes.Count];
        }
        
        public void SetValue(StatTypes type, int value, bool doNotifiy = true)
        {
            var oldValue = _data[(int) type];
            
            if (oldValue == value) return;
            
            _data[(int)type] = value;

            if (doNotifiy)
                this.PostNotification(DidChangeNotification(type), this);
        }

        public static string DidChangeNotification(StatTypes type)
        {
            if (!_didChangeNotifications.ContainsKey(type))
                _didChangeNotifications.Add(type, $"Stats.{type}DidChange");
            return _didChangeNotifications[type];
        }
    }
}