using System;

namespace Assets.Scripts.Skills
{
    // How much total XP each level needs.
    // RuneScape uses the XP curve: level 2 = 83 XP, level 50 = 101,333 XP, level 99 = 13,034,431 XP.
    public static class ExperienceTable
    {
        public const int MaxLevel = 99;

        // xpForLevel[level] = total XP needed to reach that level
        // Index is unused so the index matches the level number.
        private static readonly int[] xpForLevel = BuildTable();

        private static int[] BuildTable()
        {
            int[] table = new int[MaxLevel + 1];
            double points = 0;

            table[1] = 0; // Everyone starts at level 1 with 0 xp

            for (int level = 2; level <= MaxLevel; level++)
            {
                int previous = level - 1;
                points += Math.Floor(previous + 300.0 * Math.Pow(2.0, previous / 7.0));
                table[level] = (int)Math.Floor(points / 4.0);
            }

            return table;
        }

        // Total XP needed to reach a level (e.g. 2 -> 83 xp points)
        public static int GetXpForLevel(int level)
        {
            level = Math.Max(1, Math.Min(level, MaxLevel));
            return xpForLevel[level];
        }

        // The level a given amount of total XP gives (e.g. 90 xp points -> 2 levels):
        public static int GetLevelForXp(int xp)
        {
            for (int level = MaxLevel; level > 1; level--)
            {
                if (xp >= xpForLevel[level]) return level;
            }
            return 1;
        }
    }
}