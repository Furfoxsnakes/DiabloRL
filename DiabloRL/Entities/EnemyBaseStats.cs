namespace DiabloRL.Entities
{
    public class EnemyBaseStats
    {
        public int MonsterLevel;
        public int HealthMin;
        public int HealthMax;
        public int ArmourClass;
        public int ToHitPercent;
        public int DamageMin;
        public int DamageMax;
        // For resistances...
        // -1 = immune
        // 0 = No resistance;
        // 1 = Resistant (50%)
        public int MagicResist;
        public int FireResist;
        public int LightningResist;
        public int Experience;

        public EnemyBaseStats(int monsterLevel, int healthMin, int healthMax, int armourClass, int toHitPercent,
            int damageMin, int damageMax, int magicResist, int fireResist, int lightningResist, int experience)
        {
            MonsterLevel = monsterLevel;
            
        }
    }
}