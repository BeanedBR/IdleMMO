using Assets.Scripts.Core.Ticks;
using UnityEngine;

public sealed class TickDebugAction : MonoBehaviour, ITickable
{
    [SerializeField] private float actionSeconds = 3f;

    private TickTimer timer;
    private int completions;

    private void OnEnable()
    {
        timer = new TickTimer(TickTimer.SecondsToTicks(actionSeconds));
        timer.Start();
        TickRunner.Ticks.Register(this);
    }

    private void OnDisable()
    {
        TickRunner.Ticks.Unregister(this);
    }

    public void Tick()
    {
        if (timer.Tick())
        {
            completions++;
            Debug.Log($"[TickDebug] Action #{completions} done at tick {TickRunner.Ticks.CurrentTick} " + $"(~{Time.realtimeSinceStartup:F2}s real time)");
        }
    }
}