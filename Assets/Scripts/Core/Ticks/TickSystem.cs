using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.Core.Ticks
{
    public sealed class TickSystem
    {
        // 20 ticks per second
        public const int TicksPerSecond = 20;
        public const double SecondsPerTick = 1.0 / TicksPerSecond;

        // Total ticks processed since TickSystem was created:
        public long CurrentTick {  get; private set; }

        // Raised after every tickable has run for a tick.
        public event Action<long> Ticked;

        // Loop over a snapshot array so tickables can register/unregister
        // during a tick without breaking the loop:
        private readonly List<ITickable> tickables = new List<ITickable>();
        private ITickable[] snapshot = Array.Empty<ITickable>();
        private bool snapshotDirty;

        // Real time that has passed but hasn't yet added up to a full tick:
        private double accumulator;

        // 0 to 1: how far we are towards the next tick, based on real time (used only for smoother visuals):
        public float TickFraction => (float)Math.Min(accumulator / SecondsPerTick, 1.0);

        public void Register(ITickable tickable)
        {
            if (tickable == null || tickables.Contains(tickable)) return;
            tickables.Add(tickable);
            snapshotDirty = true;
        }

        public void Unregister(ITickable tickable)
        {
            if (tickables.Remove(tickable)) snapshotDirty = true;
        }

        // Turns frame time into ticks (used in TickRunner.cs):
        public int Advance(double deltaSeconds, int maxTicks)
        {
            if (deltaSeconds > 0) accumulator += deltaSeconds;

            int ticksRun = 0;
            while (accumulator >= SecondsPerTick && ticksRun < maxTicks)
            {
                accumulator -= SecondsPerTick;
                RunSingleTick();
                ticksRun++;
            }

            return ticksRun;
        }

        // Run a certain amount of ticks immediately, for offline progress. For example 1 hour would be 72,000 ticks.
        public void RunTicks(long count)
        {
            for (long i = 0; i < count; i++)
            {
                RunSingleTick();
            }
        }

        private void RunSingleTick()
        {
            if (snapshotDirty)
            {
                snapshot = tickables.ToArray();
                snapshotDirty = false;
            }

            CurrentTick++;

            // Loop through the snapshot table:
            for (int i = 0; i < snapshot.Length; i++)
            {
                snapshot[i].Tick();
            }

            Ticked?.Invoke(CurrentTick);
        }
    }
}