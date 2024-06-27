using DiabloRL.Scripts.Common;
using Godot;

namespace DiabloRL.Scripts.Components;

/// <summary>
/// Base class for a stat whose vcalue is relatively unchanging
/// Current will include bonus
/// example: str, agi, int, etc
/// </summary>
public abstract partial class FixedStat : StatBase {

    public int Current {
        get {
            var current = Base + BonusTotal;

            current.Clamp(GetBaseMin(), GetBaseMax());

            return current;
        }
    }
    
    protected FixedStat(int baseValue, int baseMin, int baseMax) : base(baseValue, baseMin, baseMax) {
    }
}