namespace Assets.Scripts.Core.Actions
{
    public interface IGameAction
    {
        // Name of the action:
        string Name { get; }

        // Called when the action becomes active:
        void Start();
        // Called when the player stops the action or switches to another:
        void Stop();
        // Called once per game tick while this is action is active:
        void Tick();
    }
}