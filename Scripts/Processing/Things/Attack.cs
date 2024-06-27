using Godot;
using GoRogue.DiceNotation;

namespace DiabloRL.Scripts.Processing.Things;

public partial class Attack {
    public float DamageMulti { get; private set; }

    public DiceExpression DamageDice { get; private set; }

    public Attack(DiceExpression damageDice, float damageMulti = 1.0f) {
        DamageDice = damageDice;
        DamageMulti = damageMulti;
    }

    public int Roll() {
        return Mathf.RoundToInt(DamageDice.Roll() * DamageMulti);
    }
}