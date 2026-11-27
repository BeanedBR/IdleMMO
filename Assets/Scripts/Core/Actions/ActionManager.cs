using Assets.Scripts.Core.Ticks;
using UnityEngine;

namespace Assets.Scripts.Core.Actions
{
    public sealed class ActionManager : ITickable
    {
        private static ActionManager instance;

        // Create ActionManager and register itself in the TickSystem. Actions added to the ActionManager will then load with every tick:
        public static ActionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ActionManager();
                    TickRunner.Ticks.Register(instance);
                }
                return instance;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatistics()
        {
            instance = null;
        }

        // The current action that is in the slot, it's null if there is none (player is idle, doing nothing):
        public IGameAction CurrentAction { get; private set; }

        public bool IsBusy => CurrentAction != null;

        private ActionManager() { } // Only one ActionManager from Instance

        // Puts an action into the single slot, stops whatever previous action was running in that place:
        public bool StartAction(IGameAction action)
        {
            if (action == null)
            {
                StopAction();
                return false;
            }

            if (action == CurrentAction) return true; // It's already doing this

            // Checks before stopping the current action so the player doesn't go idle if they click a locked tree:
            if (!action.CanStart(out string reason))
            {
                Debug.Log($"Can't start {action.Name}: {reason}");
                return false;
            }

            CurrentAction?.Stop(); // stop the old action if there is one
            CurrentAction = action; // swap out the old action with the new one
            CurrentAction.Start(); // start the new action
            return true;
        }

        // Empty the first slot, player is idle
        public void StopAction()
        {
            if (CurrentAction == null) return;

            CurrentAction.Stop();
            CurrentAction = null;
        }

        // Toggle action - if the action passed through is the current action then it stops else starts new action:
        public void ToggleAction(IGameAction action)
        {
            if (action != null && action == CurrentAction)
            {
                StopAction();
            }
            else
            {
                StartAction(action);
            }
        }

        // Called by the TickSystem every tick, passes it to the only action active:
        public void Tick()
        {
            CurrentAction?.Tick();
        }
    }
}