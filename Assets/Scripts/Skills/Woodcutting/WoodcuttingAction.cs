using Assets.Scripts.Core.Actions;
using Assets.Scripts.Core.Ticks;
using Assets.Data.Items;
using UnityEngine;

namespace Assets.Scripts.Skills.Woodcutting
{
    public sealed class WoodcuttingAction : IGameAction
    {
        private readonly string treeName;
        private readonly int xpPerLog;
        private readonly ItemData logItem; // ItemData (ScriptableObject)
        private readonly TickTimer timer;
        private readonly Skill skill;

        public int LogsCut {  get; private set; }
        public int XpGained { get; private set; }

        public string Name => $"Woodcutting: {treeName}";

        // 0 to 1, progress bar for later (XP & Levelling):
        public float Progress => timer.Progress;

        public WoodcuttingAction(string treeName, float secondsPerLog, int xpPerLog, ItemData logItem)
        {
            this.treeName = treeName;
            this.xpPerLog = xpPerLog;
            this.logItem = logItem;
            timer = new TickTimer(TickTimer.SecondsToTicks(secondsPerLog));
            skill = SkillManager.Instance.Get(SkillType.Woodcutting);
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

            Bank.Instance.Add(logItem); // add item to player's bank
            LogsCut++;
            XpGained += xpPerLog;

            int levelsGained = skill.AddXp(xpPerLog);

            Debug.Log($"{Name}: log cut! Bank now has {Bank.Instance.GetQuantity(logItem)}. " +
                $"Woodcutting XP: {skill.Xp}");

            if (levelsGained > 0)
            {
                Debug.Log($"Level up! Woodcutting is now level {skill.Level}");
            }
        }

        public bool CanStart(out string reason)
        {
            throw new System.NotImplementedException();
        }
    }
}