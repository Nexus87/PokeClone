using GameEngine.Core.ECS.Actions;
using GameEngine.Core.ECS.Components;

namespace GameEngine.Core.ECS.Systems
{
    public class BlockingQueueingSystem
    {
        private readonly IMessageBus _messageBus;

        public BlockingQueueingSystem(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }
        public void QueueAction(QueueAction action, IEntityManager entityManager)
        {
            var component = entityManager.GetFirstComponentOfType<ActionQueueComponent>();
            component.Actions.Enqueue(action.Action);
        }

        public void Update(IEntityManager entityManager)
        {
            var component = entityManager.GetFirstComponentOfType<ActionQueueComponent>();
            if(!component.IsBlocked && component.Actions.Count > 0)
            {
                component.IsBlocked = true;
                _messageBus.SendAction(component.Actions.Dequeue());
            }
        }
        public void Unblock(IEntityManager entityManager)
        {
            var component = entityManager.GetFirstComponentOfType<ActionQueueComponent>();
            component.IsBlocked = false;            
        }
    }
}