namespace GameEngine.Core.ECS.Actions
{
    public class QueueAction
    {
        public readonly object Action;
        public QueueAction(object action)
        {
            Action = action;
        }
    }
}