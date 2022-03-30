using System;
using System.Linq;
using DiabloRL.Resources;
using Action = DiabloRL.Actions.Action;

namespace DiabloRL.Components.Stats;

public class Experience : FluidStat
{
    public override int TotalMax => int.MaxValue;
    public override int TotalMin => 0;

    public static string NotificationString = "ExperienceChanged";

    public int Level => _level;
    private int _level = 1;

    public Experience(int baseValue) : base(baseValue)
    {
        
    }

    private int LevelForExperience(int exp)
    {
        var lvl = Game.Content.ExperienceData.Length - 1;

        for (var i = lvl; i >= 0; i--)
            if (Game.Content.ExperienceData[i] > exp)
                lvl--;

        return lvl + 1;
    }

    protected override void OnChanged()
    {
        base.OnChanged();
        
    }
    
    public void Gain(Action action, int amount)
    {
        // do not allow to go over the max experience possible
        Current = Math.Min(Current + amount, Game.Content.ExperienceData.Last());
        // check if the entity leveled up
        RefreshLevel(action);
    }

    public void RefreshLevel(Action action)
    {
        var newLevel = LevelForExperience(Current);

        // don't do anything if still at same level
        if (newLevel == _level) return;

        // somehow we lost a level?
        if (newLevel < _level) return;
        
        // level up!
        while (_level < newLevel)
        {
            _level++;
            action.Log($"{Parent.Name} has reached level {_level}!");
        }
    }

    public override int GetMax()
    {
        return TotalMax;
    }
}