namespace DiabloRL.Components.Stats;

public class Armour : FluidStat
{
    public Armour(int baseValue) : base(baseValue)
    {
        
    }

    public override int GetMax()
    {
        var dexFromStat = 0f;
        var dexStat = Parent.AllComponents.GetFirstOrDefault<Dexterity>();
        if (dexStat != null)
            dexFromStat = dexStat.Current / 5;
        
        // get armour items
        var armourFromItems = 0;

        return (int) (dexFromStat + armourFromItems);
    }
}