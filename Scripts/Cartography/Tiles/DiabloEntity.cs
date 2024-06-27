using System.Collections.Generic;
using DiabloRL.Scripts.Components;
using DiabloRL.Scripts.Interfaces;
using DiabloRL.Scripts.Processing;
using DiabloRL.Scripts.Processing.Behaviours;
using DiabloRL.Scripts.Processing.Things;
using Godot;
using GoRogue.DiceNotation;
using GoRogue.DiceNotation.Terms;
using GoRogue.FOV;
using SadRogue.Primitives;

namespace DiabloRL.Scripts.Cartography.Tiles;

public partial class DiabloEntity : DiabloGameObject, ISpeed {

    private Behaviour _behaviour;
    public Behaviour Behaviour => _behaviour;
    
    public FluidStat Life { get; private set; }
    public FluidStat Mana { get; private set; }
    public FluidStat Experience { get; private set; }
    
    public int Speed { get; private set; }
    private Speed _speed;

    public Stats Stats { get; private set; }

    public bool IsAlive => Life.Current > 0;
    
    public Energy Energy { get; private set; }

    public DiabloEntity(Point pos, GameObjectDetails details) : base(pos, details, 1) {
        Life = new FluidStat(details.StartingLife, 0, 999);
        Mana = new FluidStat(details.StartingMana, 0, 999);
        Experience = new FluidStat(0, 0, 999999);
        Energy = new Energy(details.Speed);

        Stats = new Stats(details.BaseStrength, details.BaseMagic, details.BaseDexterity, details.BaseVitality);

        PositionChanged += OnPositionChanged;
        PositionChanging += OnPositionChanging;
    }

    public virtual void OnPositionChanging(object sender, ValueChangedEventArgs<Point> e) {
        
    }

    public virtual void OnPositionChanged(object sender, ValueChangedEventArgs<Point> e) {
    }

    public override void OnPlayerFovCalcuated(IFOV fov) {
        Visible = fov.BooleanResultView[Position];
    }

    public void SetBehaviour(Behaviour behaviour) {
        _behaviour = behaviour;
    }

    public void SetNextAction(Action action) {
        SetBehaviour(new OneShotBehaviour(action));
    }

    public IEnumerable<Action> TakeTurn() {
        var turnAction = _behaviour.NextAction();
        yield return turnAction;
    }

    public virtual int ReceiveDamage(Attack attack) {
        float baseAmount = attack.Roll();
        
        //TODO: get armour reduction

        // round up so that anything under 1.0 is not cancelled out
        var appliedDamage = Mathf.CeilToInt(baseAmount);
        
        //TODO: remove applied damage from health
        Life.Current -= appliedDamage;
        GD.Print($"{Details.Name} took {appliedDamage} damage. They have {Life.Current} life remaining.");

        if (Life.Current == 0) {
            Die();
        }

        return appliedDamage;
    }

    public virtual void Die() {
        GD.Print($"{Details.Name} has been slain!");
    }

    public virtual Attack GetAttack(DiabloEntity defender) {
        var dice = Dice.Parse("1d3");
        var attack = new Attack(dice);
        return attack;
    }
}