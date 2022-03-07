namespace DiabloRL.Components.Stats;

public class Stat : FixedStat
{
    private int _baseMin;
    public static implicit operator int(Stat stat) => stat.Current;

    public override int BaseMin => _baseMin;

    public Stat(int baseValue) : base(baseValue)
    {
        
    }
}