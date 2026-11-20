using System;

namespace Assets.Scripts.Core.Ticks
{
    public sealed class TickTimer
    {
        public int IntervalTicks { get; private set; }  // The finishline (for example 60 ticks)
        public int ElapsedTicks { get; private set; }   // The current tick (for example 32 / 60 ticks)
        public bool IsRunning { get; private set; }

        public float Progress => IntervalTicks == 0 ? 0f : (float)ElapsedTicks / IntervalTicks;

        public TickTimer(int intervalTicks)
        {
            SetInterval(intervalTicks);
        }

        // Change the interval, e.g. when an upgrade makes an action faster
        public void SetInterval(int intervalTicks)
        {
            IntervalTicks = Math.Max(1, intervalTicks);
        }

        // Convert seconds into ticks (3 seconds returns 60 ticks)
        public static int SecondsToTicks(double seconds)
        {
            return Math.Max(1, (int)Math.Round(seconds * TickSystem.TicksPerSecond));
        }

        public void Start()
        {
            ElapsedTicks = 0;
            IsRunning = true;
        }

        public void Stop()
        {
            ElapsedTicks = 0;
            IsRunning = false;
        }

        // Called once per game tick. Returns true on the tick
        public bool Tick()
        {
            if (!IsRunning) return false;
            ElapsedTicks++;
            if (ElapsedTicks < IntervalTicks) return false;
            ElapsedTicks = 0;
            return true;
        }
    }
}