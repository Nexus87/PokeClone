using GameEngine.Core.ECS.Actions;
using GameEngine.Core.ECS.Components;

namespace GameEngine.Core.ECS.Systems
{
    public class BlockingQueueingSystem
    {
        public static void RegisterHandler(IMessageBus messageBus)
        {
            messageBus.RegisterForAction<TimeAction>(BlockingQueueingSystem.Update);
            messageBus.RegisterForAction<QueueAction>(BlockingQueueingSystem.QueueAction);
            messageBus.RegisterForAction<UnblockQueueAction>(BlockingQueueingSystem.Unblock);

        }
        public static void QueueAction(QueueAction action, IEntityManager entityManager, IMessageBus messageBus)
        {
            var component = entityManager.GetFirstComponentOfType<ActionQueueComponent>();
            component.Actions.Enqueue(action.Action);
        }

        public static void Update(IEntityManager entityManager, IMessageBus messageBus)
        {
            var component = entityManager.GetFirstComponentOfType<ActionQueueComponent>();
            if (!component.IsBlocked && component.Actions.Count > 0)
            {
                component.IsBlocked = true;
                messageBus.SendAction(component.Actions.Dequeue());
            }
        }
        public static void Unblock(IEntityManager entityManager, IMessageBus messageBus)
        {
            var component = entityManager.GetFirstComponentOfType<ActionQueueComponent>();
            component.IsBlocked = false;
        }
    }
}