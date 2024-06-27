using System;
using DiabloRL.Scripts.Interfaces;
using Godot;

namespace DiabloRL.Scripts.Components;

public partial class Energy {
    private class FixedSpeed : ISpeed {
        private int _speed;
        public int Speed => _speed;

        public FixedSpeed(int speed) {
            _speed = speed;
        }
    }
    
    public const int MinSpeed = 0;
    public const int NormalSpeed = 6;
    public const int MaxSpeed = 12;
    public const int ActionCost = 240;

    public static int GetGain(int speed) => EnergyGains[speed];

    public bool HasEnoughEnergyForAction => _energy >= ActionCost;
    public int Current => _energy;

    private ISpeed _speed;
    private int _energy;

    private static readonly int[] EnergyGains = {
        15,     // 1/4 normal speed
        20,     // 1/3 normal speed
        25,
        30,     // 1/2 normal speed
        40,
        50,
        60,     // normal speed
        80,
        100,
        120,    // 2x normal speed
        150,
        180,    // 3x normal speed
        240     // 4x normal speed
    };

    public Energy(ISpeed speed) {
        if (speed == null) {
            throw new ArgumentNullException("speed");
        }

        _speed = speed;
    }

    public Energy(int speed) {
        _speed = new FixedSpeed(speed);
    }

    public void Gain() {
        var speed = _speed.Speed;
        _energy += EnergyGains[speed];
    }

    public void Reset() => _energy = 0;

    public void Fill() => _energy = ActionCost;

    public void Spend() => _energy -= ActionCost;
}