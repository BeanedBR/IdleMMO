using Assets.Scripts.Core.Actions;
using Assets.Scripts.Core.Ticks;
using UnityEngine;

namespace Assets.Scripts.Skills.Woodcutting
{
    public sealed class WoodcuttingAction : IGameAction
    {
        private readonly string treeName;
        private readonly int xpPerLog;
        private readonly TickTimer timer;

        public int LogsCut {  get; private set; }
        public int XpGained { get; private set; }

        public string Name => $"Woodcutting: {treeName}";

        public float Progress => timer.Progress;

        public WoodcuttingAction(string treeName, float secondsPerLog, int xpPerLog)
        {
            this.treeName = treeName;
            this.xpPerLog = xpPerLog;
            timer = new TickTimer(TickTimer.SecondsToTicks(secondsPerLog));
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
            if (!timer.Tick()) return; //

            LogsCut++;
            XpGained += xpPerLog;
            Debug.Log($"{Name}: log cut. ({LogsCut} logs, {XpGained} XP.)");
        }
    }
}