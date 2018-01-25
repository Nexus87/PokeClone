namespace GameEngine.Core.ECS.Actions
{
    public class QueueAction
    {
        public readonly object Action;
        public readonly object Sender;

        public QueueAction(object action, object sender = null)
        {
            Action = action;
            Sender = sender;
        }
    }
}