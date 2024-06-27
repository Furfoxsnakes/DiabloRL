using Godot;

namespace DiabloRL.Scripts.Components;

public partial class Speed : FixedStat {

    public static implicit operator int(Speed speed) => speed.Current;
    
    public Speed(int baseValue, int baseMin, int baseMax) : base(baseValue, baseMin, baseMax) {
        
    }
}