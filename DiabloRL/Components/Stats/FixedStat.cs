using System;

namespace DiabloRL.Components.Stats;

public class FixedStat : StatBaseComponent
{
    public int Current => Base;

    public FixedStat(int baseValue) : base(baseValue)
    {
        
    }

    private int _current;
}