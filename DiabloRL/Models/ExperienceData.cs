namespace DiabloRL.Models
{
    public class ExperienceData
    {
        private static int[] _table = new[]
        {
            0,
            2000,
            4620,
            8040,
            12489,
            18258,
            25712,
            35309,
            47622,
            63364,
            83419,
            108879,
            141086,
            181683,
            231075,
            313656,
            424067,
            579190,
            766569,
            1025154,
            1366277,
            1814568,
            2401895,
            3168651,
            4166200
        };

        public static int ExperienceForLevel(int level)
        {
            var index = level - 1;
            if (index < 0 || index > _table.Length) return 0;

            return _table[index];
        }

        public static int LevelForExperience(int Exp)
        {
            var level = 25;
            for(; level >= 1; --level)
                if (Exp >= ExperienceForLevel(level))
                    break;
            return level;
        }
    }
}