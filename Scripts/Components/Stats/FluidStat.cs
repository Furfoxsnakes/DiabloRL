using DiabloRL.Scripts.Common;
using Godot;

namespace DiabloRL.Scripts.Components;

public partial class FluidStat : StatBase {

    public int Min => 0;
    public int Max => Base + BonusTotal;

    private int _current;
    public int Current {
        get => _current;
        set {
            value = value.Clamp(Min, Max);

            if (_current == value) return;

            _current = value;
            OnChanged();
        }
    }

    public bool IsMax => _current == Max;
    
    public FluidStat(int baseValue, int baseMin, int baseMax) : base(baseValue, baseMin, baseMax) {
        Current = Base;
    }

    protected override void OnBaseChanged() {
        KeepCurrentInBounds();
    }

    protected override void OnBonusChanged() {
        base.OnBonusChanged();
        
        KeepCurrentInBounds();
    }

    private void KeepCurrentInBounds() {
        _current = _current.Clamp(Min, Max);
    }
}