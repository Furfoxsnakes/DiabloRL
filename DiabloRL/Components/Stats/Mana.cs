namespace DiabloRL.Components.Stats;

public class Mana : FluidStat
{
    public override int GetMax()
    {
        var magicFromStat = Parent.AllComponents.GetFirstOrDefault<Vitality>().Current * _magicStatModifier;
            
        //get +Vitality from equipped items
        //TODO: get items from components
        var magicFromItems = 0 * _magicItemsModifier;
            
        //get hero level
        //TODO: hardcoded for now but will move to a class component in future
        var levelBonus = 1 * _characterLevelModifier;
            
        // get +Mana from equipped items
        //TODO: get items from components
        var manaFromItems = 0;

        return (int)(magicFromStat + magicFromItems + levelBonus + manaFromItems + _baseModifier);
    }

    public Mana(float magicStatModifer, float magicItemsModifier, float characterLevelModifier, int baseModifier, int gainOnLevel) : base(0)
    {
        _magicStatModifier = magicStatModifer;
        _magicItemsModifier = magicItemsModifier;
        _characterLevelModifier = characterLevelModifier;
        _baseModifier = baseModifier;
        _gainOnLevel = gainOnLevel;
    }

    private float _magicStatModifier;
    private float _magicItemsModifier;
    private float _characterLevelModifier;
    private int _baseModifier;
    private int _gainOnLevel;
}