using UnityEngine;

namespace Assets.Scripts.Core.Ticks
{
    [DefaultExecutionOrder(-100)]
    public sealed class TickRunner : MonoBehaviour
    {
        [SerializeField, Tooltip("Safety cap so one very long frame doesn't freeze the game " + "Any extra time carries over to the following frames.")]
        private int maxTicksPerFrame = 200;

        private static TickSystem ticks;
        private static TickRunner instance;

        public static TickSystem Ticks => ticks ??= new TickSystem();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatistics()
        {
            ticks = null;
            instance = null;
        }

        private void Awake()
        {
            // Tickrunner already exists so remove this duplicate and start a new one:
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject); // the clock survives scene changes
        }

        private void Update()
        {
            Ticks.Advance(Time.unscaledDeltaTime, maxTicksPerFrame);
        }
    }
}

