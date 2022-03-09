using System;
using GoRogue.DiceNotation;
using SadRogue.Primitives;

namespace DiabloRL.Things;

public class Attack
{
    private string _verb;
    private Element _element;
    private EffectType _type;
    private float _damageBonus;
    private DiceExpression _damage;

    public string Verb => _verb;
    public EffectType Type => _type;
    public Element Element => _element;

    public Attack(DiceExpression damage, float damageBonus, Element element, EffectType type)
    {
        _damage = damage;
        _damageBonus = damageBonus;
        _element = element;
        _type = type;
    }

    public int Roll() => (int) Math.Round(_damage.Roll() * _damageBonus);
}