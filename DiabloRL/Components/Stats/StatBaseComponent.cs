using System;
using DiabloRL.Entities;
using SadRogue.Integration.Components;

namespace DiabloRL.Components.Stats
{
    public abstract class StatBaseComponent : RogueLikeComponentBase<GameEntity>
    {
        internal event EventHandler Changed;
        internal event EventHandler BonusChanged;

        public virtual int BaseMin => 0;
        public virtual int BaseMax => 255;

        public virtual int TotalMin => 0;
        public virtual int TotalMax => 255;

        public int Base
        {
            get => _base;
            set
            {
                value = Math.Clamp(value, BaseMin, BaseMax);
                if (_base == value) return;
                _base = value;
                OnChanged();
                OnBaseChanged();
            }
        }
        
        public StatBaseComponent(int baseValue) : base(isUpdate: false, isRender: false, isMouse: false, isKeyboard: false)
        {
            _base = baseValue;
        }

        protected virtual void OnChanged() => Changed?.Invoke(this, EventArgs.Empty);
        
        protected virtual void OnBaseChanged() { }

        private int _base;
    }
}