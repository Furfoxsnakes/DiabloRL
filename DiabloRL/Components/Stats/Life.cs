namespace DiabloRL.Components.Stats;

public class Life : FluidStat
{
    public override int GetMax()
    {
        // add vitality from stat if possible
        var vitalityFromStat = 0f;
        if (Parent.AllComponents.Contains<Vitality>())
            vitalityFromStat = Parent.AllComponents.GetFirstOrDefault<Vitality>().Current * _vitalityStatModifier;

        //get +Vitality from equipped items
        //TODO: get items from components
        var vitalityFromItems = 0 * _vitalityItemsModifier;
            
        //get hero level
        //TODO: hardcoded for now but will move to a class component in future
        var levelBonus = 1 * _characterLevelModifier;
            
        // get +Life from equpped items
        //TODO: get items from components
        var lifeFromItems = 0;

        return (int)(vitalityFromStat + vitalityFromItems + levelBonus + lifeFromItems + _baseModifier + Base);
    }

    /// <summary>
    /// Life component used for players.
    /// Calculates dynamically based on stats, character level and items
    /// </summary>
    /// <param name="vitalityStatModifer"></param>
    /// <param name="vitalityItemsModifier"></param>
    /// <param name="characterLevelModifier"></param>
    /// <param name="baseModifier"></param>
    /// <param name="gainOnLevel"></param>
    public Life(float vitalityStatModifer, float vitalityItemsModifier, float characterLevelModifier, int baseModifier, int gainOnLevel) : base(0)
    {
        _vitalityStatModifier = vitalityStatModifer;
        _vitalityItemsModifier = vitalityItemsModifier;
        _characterLevelModifier = characterLevelModifier;
        _baseModifier = baseModifier;
        _gainOnLevel = gainOnLevel;
    }

    /// <summary>
    /// Life component used for things like Monsters and objects
    /// Fixed amount
    /// </summary>
    /// <param name="baseValue"></param>
    public Life(int baseValue) : base(baseValue)
    {
    }

    private float _vitalityStatModifier;
    private float _vitalityItemsModifier;
    private float _characterLevelModifier;
    private int _baseModifier;
    private int _gainOnLevel;
}