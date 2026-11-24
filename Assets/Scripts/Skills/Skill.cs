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

        public int AddXp(int amount)
        {
            if (amount <= 0) return 0;

            // Add as a long first so a huge total can't overflow:
            Xp = (int)Math.Min((long)Xp + amount, int.MaxValue);

            int oldLevel = Level;
            // Create Experience table to get level for xp
            return Level - oldLevel;
        }
    }
}