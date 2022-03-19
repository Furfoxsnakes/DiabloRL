using DiabloRL.Actions;
using DiabloRL.Behaviors.Monster;
using DiabloRL.Components.Stats;
using DiabloRL.Things;
using GoRogue.DiceNotation;
using SadRogue.Primitives;

namespace DiabloRL.Entities;

public class Zombur : Enemy
{
    public Zombur() : base(Color.WhiteSmoke, Color.Black, 'z')
    {
        Name = "Zombur";
        
        SetBehavior(new ZombieBehavior(this));
        
        // add components
        AllComponents.Add(new Energy(40));
        AllComponents.Add(new Life(5));
        AllComponents.Add(new Intelligence(3));
        AllComponents.Add(new Experience(10));
        
        // init their stats
        Life.Current = Life.Max;
    }

    public override Attack GetAttack(GameEntity defender)
    {
        var damage = Dice.Parse("1d3");
        var element = Element.None;
        var effectType = EffectType.Melee;
            
        // get modifiers based on equipped weapon(s)
            
        // build the attack
        var attack = new Attack(damage, 1, element, effectType);
            
        // allow the defender to modify the attack
            
        // send it back
        return attack;
    }
}