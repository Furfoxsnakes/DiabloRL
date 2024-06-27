using Godot;

namespace DiabloRL.Scripts.Components;

public partial class Stat : FixedStat {

    public static implicit operator int(Stat stat) => stat.Current;

    public string Name => GetType().Name;
    
    public Stat(int baseValue, int baseMin, int baseMax) : base(baseValue, baseMin, baseMax) {
    }

    /// <summary>
    /// Initialize a debug stat with a fixed base
    /// </summary>
    public Stat() : this(10, 10, 500) {
        
    }
}