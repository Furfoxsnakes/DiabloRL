using System;
using System.Collections.Generic;
using System.Linq;
using DiabloRL.Scripts.Common;
using DiabloRL.Scripts.Enums;
using Godot;

namespace DiabloRL.Scripts.Components;

public abstract partial class StatBase {
    private event EventHandler Changed;
    private event EventHandler BonusChanged;

    private int _base;

    private int _baseMin;
    private int _baseMax;

    public int Base {
        get => _base;
        set {
            value = value.Clamp(GetBaseMin(), GetBaseMax());
            
            if (_base == value) return;

            _base = value;
            OnBaseChanged();
            OnChanged();
        }
    }

    private Dictionary<BonusType, int> _bonuses = new Dictionary<BonusType, int>();
    public int BonusTotal => _bonuses.Values.Sum();

    public StatBase(int baseValue, int baseMin, int baseMax) {
        _base = baseValue.Clamp(baseMin, baseMax);
        _baseMin = baseMin;
        _baseMax = baseMax;
    }

    /// <summary>
    /// Checks wether a bonus type exists and it's not 0
    /// </summary>
    /// <param name="bonusType"></param>
    /// <returns>True if the bonus type exists and is not 0</returns>
    public bool HasBonusType(BonusType bonusType) {
        if (_bonuses.ContainsKey(bonusType)) {
            return _bonuses[bonusType] != 0;
        }

        return false;
    }

    public int GetBonusOfType(BonusType bonusType) {
        return !HasBonusType(bonusType) ? 0 : _bonuses[bonusType];
    }

    public void SetBonusOfType(BonusType bonusType, int bonusValue) {
        // set the bonus type value if not already
        if (!_bonuses.ContainsKey(bonusType)) {
            _bonuses[bonusType] = 0;
        }

        // raise proper events if the value being set is different
        if (_bonuses[bonusType] != bonusValue) {
            _bonuses[bonusType] = bonusValue;
            OnBonusChanged();
            OnChanged();
        }
    }

    public void AddBonus(BonusType bonusType, int bonusValue) {
        if (bonusValue == 0) return;

        if (!_bonuses.ContainsKey(bonusType)) {
            _bonuses[bonusType] = 0;
        }

        _bonuses[bonusType] += bonusValue;
        
        OnBonusChanged();
        OnChanged();
    }
    
    protected void OnChanged() {
        Changed?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnBonusChanged() {
        BonusChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Override to return the minimum value this stat can have
    /// </summary>
    /// <returns></returns>
    public int GetBaseMin() => _baseMin;

    /// <summary>
    /// Override to return the maximum value this stat can have
    /// </summary>
    /// <returns></returns>
    public int GetBaseMax() => _baseMax;

    /// <summary>
    /// Allow the derived class to make any validation checks
    /// </summary>
    protected virtual void OnBaseChanged() {
        
    }
}