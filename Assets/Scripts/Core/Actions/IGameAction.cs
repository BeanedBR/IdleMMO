namespace Assets.Scripts.Core.Actions
{
    public interface IGameAction
    {
        // Name of the action:
        string Name { get; }

        // Can the player start this action? If not then display the reason and return false:
        bool CanStart(out string reason);

        // Called when the action becomes active:
        void Start();
        // Called when the player stops the action or switches to another:
        void Stop();
        // Called once per game tick while this is action is active:
        void Tick();
    }
}