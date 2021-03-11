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
                this.PostNotification(DidChangeNotification(type), oldValue);
        }

        public static string DidChangeNotification(StatTypes type)
        {
            if (!_didChangeNotifications.ContainsKey(type))
                _didChangeNotifications.Add(type, $"Stats.{type}DidChange");
            return _didChangeNotifications[type];
        }
    }
}