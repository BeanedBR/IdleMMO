using System;

namespace Assets.Scripts.Skills
{
    public sealed class Skill
    {
        public SkillType Type { get; }
        public int Xp { get; private set; }
        public int Level { get; private set; } = 1;

        public Skill(SkillType type)
        {
            Type = type;
        }

        // 0 to 1
        public float ProgressToNextLevel
        {
            get
            {
                if (Level >= ExperienceTable.MaxLevel) return 1f;

                int currentLevelXp = ExperienceTable.GetXpForLevel(Level);
                int nextLevelXp = ExperienceTable.GetXpForLevel(Level + 1);
                return (float)(Xp - currentLevelXp) / (nextLevelXp - currentLevelXp);
            }
        }

        public int AddXp(int amount)
        {
            if (amount <= 0) return 0;

            // Add as a long first so a huge total can't overflow:
            Xp = (int)Math.Min((long)Xp + amount, int.MaxValue);

            int oldLevel = Level;
            Level = ExperienceTable.GetLevelForXp(Xp);
            return Level - oldLevel;
        }
    }
}