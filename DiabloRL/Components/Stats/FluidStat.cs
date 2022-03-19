using System;
using System.Transactions;

namespace DiabloRL.Components.Stats;

public abstract class FluidStat : StatBaseComponent
{
    public int Max => GetMax();

    public int Current
    {
        get => _current;
        set
        {
            value = Math.Clamp(value, 0, Max);
            if (_current != value)
            {
                _current = value;
                OnChanged();
            }
        }
    }

    public FluidStat(int baseValue) : base(baseValue)
    {
        _current = baseValue;
    }

    public abstract int GetMax();

    private int _current;
}