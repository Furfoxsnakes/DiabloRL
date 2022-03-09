using System;
using DiabloRL.Entities;
using SadRogue.Primitives;

namespace DiabloRL.Things;

public class Hit
{
    private int? _damage;
    private Attack _attack;
    private GameEntity _attacker;
    private bool _canDodge;
    private Direction _direction;

    public Attack Attack => _attack;
    public GameEntity Attacker => _attacker;
    public bool CanDodge => _canDodge;
    public Direction Direction => _direction;

    public int Damage
    {
        get
        {
            if (!_damage.HasValue) throw new InvalidOperationException("Cannot access hit damage before it's been set.");

            return _damage.Value;
        }
    }

    public Hit(GameEntity attacker, Attack attack, bool canDodge, Direction dir)
    {
        _attacker = attacker;
        _attack = attack;
        _canDodge = canDodge;
        _direction = dir;
    }

    /// <summary>
    /// Creates an appropriate effect for this hit at the given position
    /// </summary>
    /// <param name="pos">Position of the effect</param>
    /// <returns></returns>
    public Effect CreateEffect(Point pos) => new Effect(pos, _direction, Attack.Type, Attack.Element);

    public void SetDamage(int damage) => _damage = damage;
}