namespace DiabloRL.Components.Stats;

public class Experience : FluidStat
{
    public override int TotalMax => int.MaxValue;
    public override int TotalMin => 0;

    public Experience(int baseValue) : base(baseValue)
    {
        
    }

    public override int GetMax()
    {
        return TotalMax;
    }
}