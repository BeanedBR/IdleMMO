using Assets.Scripts.Core.Actions;
using Assets.Scripts.Core.Ticks;
using Assets.Data.Items;
using UnityEngine;

namespace Assets.Scripts.Skills.Woodcutting
{
    public sealed class WoodcuttingAction : IGameAction
    {
        private readonly TreeData tree;
        private readonly TickTimer timer;
        private readonly Skill skill;

        public int LogsCut {  get; private set; }
        public int XpGained { get; private set; }

        public TreeData Tree => tree;
        public string Name => $"Woodcutting: {tree.DisplayName}";

        // 0 to 1, progress bar for later (XP & Levelling):
        public float Progress => timer.Progress;
        public float SmoothProgress => timer.GetSmoothProgress(TickRunner.Ticks.TickFraction);

        public WoodcuttingAction(TreeData tree)
        {
            this.tree = tree;
            timer = new TickTimer(TickTimer.SecondsToTicks(tree.SecondsPerLog));
            skill = SkillManager.Instance.Get(SkillType.Woodcutting);
        }

        // Checks whether the player can start this action (can't start if they are not the required level the tree requires)
        public bool CanStart(out string reason)
        {
            if (tree.LogItem == null)
            {
                reason = $"{tree.DisplayName} has no log item set in its asset.";
                return false;
            }

            if (skill.Level < tree.LevelRequired)
            {
                reason = $"Requires:\n Woodcutting level {tree.LevelRequired}";
                return false;
            }

            reason = string.Empty;
            return true;
        }

        public void Start()
        {
            timer.Start();
            Debug.Log($"Started {Name}");
        }

        public void Stop()
        {
            timer.Stop();
            Debug.Log($"Stopped {Name}. Total: {LogsCut} logs, {XpGained} XP");
        }

        public void Tick()
        {
            if (!timer.Tick()) return; // not finished cutting yet

            Bank.Instance.Add(tree.LogItem); // add item to player's bank
            LogsCut++;
            XpGained += tree.XPPerLog;

            int levelsGained = skill.AddXp(tree.XPPerLog);

            Debug.Log($"{Name}: log cut! Bank now has {Bank.Instance.GetQuantity(tree.LogItem)}. " +
                $"Woodcutting XP: {skill.Xp}");

            if (levelsGained > 0)
            {
                Debug.Log($"Level up! Woodcutting is now level {skill.Level}");
            }
        }
    }
}