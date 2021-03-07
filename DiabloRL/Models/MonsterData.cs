using System.Collections.Generic;
using DiabloRL.Enums;

namespace DiabloRL.Models
{
    public class MonsterData
    {
        public int DungeonLevelMin;
        public int DungeonLevelMax;
        public int MonsterLevel;
        public Dictionary<Difficulties, int[]> HealthRange;
        public Dictionary<Difficulties, int> ArmourClass;
        public Dictionary<Difficulties, int> ToHit;
        public Dictionary<Difficulties, int[]> DamageRange;
        public Dictionary<Difficulties, int> MagicResistance;
        public Dictionary<Difficulties, int> FireResistance;
        public Dictionary<Difficulties, int> LightningResistance;
        public Dictionary<Difficulties, int> BaseExperience;
    }
}