using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    public class SkillManager
    {
        private static SkillManager instance;

        public static SkillManager Instance => instance ??= new SkillManager();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatistics()
        {
            instance = null;
        }

        private readonly Dictionary<SkillType, Skill> skills = new Dictionary<SkillType, Skill>();

        // Every skill, for printing now and the UI later:
        public IEnumerable<Skill> All => skills.Values;
        
        private SkillManager()
        {
            // Create one Skill (level 1, 0 XP) for every entry in SkillType:
            foreach (SkillType type in Enum.GetValues(typeof(SkillType)))
            {
                skills[type] = new Skill(type);
            }
        }

        // Get a skill, Get(SkillType.Woodcutting).
        public Skill Get(SkillType type) => skills[type];
    }
}

