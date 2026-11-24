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
    }
}

