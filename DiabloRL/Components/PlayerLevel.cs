using System;
using DiabloRL.Enums;
using DiabloRL.Models;
using SadConsole.Components.GoRogue;

namespace DiabloRL.Components
{
    public class PlayerLevel : ComponentBase<Player>
    {
        public const int MinLevel = 1;
        public const int MaxLevel = 25;
        public const int MaxExperience = 1583495809;    // from Jarulf's PDF

        public int Level
        {
            get => Parent.Stats[StatTypes.LEVEL];
            set => Parent.Stats[StatTypes.LEVEL] = value;
        }

        public int Exp
        {
            get => Parent.Stats[StatTypes.EXPERIENCE];
            set => Parent.Stats[StatTypes.EXPERIENCE] = Math.Min(value, MaxExperience);
        }

        public float LevelPercent =>
            (float)ExperienceData.ExperienceForLevel(Level) / ExperienceData.ExperienceForLevel(Level + 1);

        public PlayerLevel()
        {
            this.AddObserver(OnExpDidChange, Stats.DidChangeNotification(StatTypes.EXPERIENCE));
            this.AddObserver(OnLevelDidChange, Stats.DidChangeNotification(StatTypes.LEVEL));
        }

        private void OnLevelDidChange(object sender, object args)
        {
            var prevLevel = (int) args;
            var numLevels = Level - prevLevel;
            for (var i = 0; i < numLevels; ++i)
                LevelUp();
        }

        private void OnExpDidChange(object sender, object args)
        {
            Level = ExperienceData.LevelForExperience(Exp);
        }

        private void LevelUp()
        {
            System.Console.WriteLine($"{Parent.Name} has leveled up and is now level {Level}!");
            Parent.Stats[StatTypes.MAX_LIFE] += Parent.Stats[StatTypes.LGOL];
            Parent.Stats[StatTypes.MAX_MANA] += Parent.Stats[StatTypes.MGOL];
            Parent.Stats[StatTypes.LIFE] = Parent.Stats[StatTypes.MAX_LIFE];
            Parent.Stats[StatTypes.MANA] = Parent.Stats[StatTypes.MAX_MANA];
        }

        public void Init(int level)
        {
            Parent.Stats.SetValue(StatTypes.LEVEL, level, false);
            Exp = ExperienceData.ExperienceForLevel(level);
        }
    }
}