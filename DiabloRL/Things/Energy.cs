using System;
using SadRogue.Integration.Components;

namespace DiabloRL.Things;

public class Energy : RogueLikeComponentBase
{
    private int _energy;
    private int _gain;
    
    public const int ActionCost = 100;

    public bool HasEnergy => _energy >= ActionCost;
    public int Curent => _energy;

    public Energy(int gain) : base(false, false, false, false)
    {
        _gain = gain;
    }

    public void Gain()
    {
        _energy += _gain;
    }

    public void Fill()
    {
        // don't overwrite if the energy is already over the ActionCost
        if (_energy > ActionCost) return;
            
        _energy = ActionCost;
    }

    public void Spend()
    {
        _energy = Math.Min(0, _energy - ActionCost);
    }
}