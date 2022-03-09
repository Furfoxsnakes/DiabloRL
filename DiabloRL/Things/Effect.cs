using SadRogue.Primitives;

namespace DiabloRL.Things;

public enum EffectType
{
    Melee,
    Arrow,
    Bolt
}

public class Effect
{
    public Point Position => _position;
    public Direction Direction => _direction;
    public EffectType Type => _type;
    public Element Element => _element;

    public Effect(Point pos, Direction dir, EffectType type, Element element)
    {
        _position = pos;
        _direction = dir;
        _type = type;
        _element = element;
    }

    public Effect(Point pos, EffectType type, Element element) : this(pos, Direction.None, type, element)
    {
        
    }

    private Point _position;
    private Direction _direction;
    private EffectType _type;
    private Element _element;
}