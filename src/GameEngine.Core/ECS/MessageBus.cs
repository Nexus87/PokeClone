using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameEngine.Core.ECS
{
    public class MessageBus : IMessageBus
    {
        public int ActionCount => _messageQueue.Count;

        private readonly IEntityManager _entityManager;
        private readonly ConcurrentQueue<object> _messageQueue = new ConcurrentQueue<object>();

        private readonly Dictionary<Type, List<Action<object, IEntityManager>>> _handlers = 
            new Dictionary<Type, List<Action<object, IEntityManager>>>();

        private readonly Dictionary<object, Action<object, IEntityManager>> _handlerMap =
            new Dictionary<object, Action<object, IEntityManager>>();

        private readonly Task _processingTask;

        public MessageBus(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _processingTask = new Task(QueueProcessor, TaskCreationOptions.LongRunning);
            _processingTask.Start();
        }

        private void QueueProcessor()
        {
            while (true)
            {
                object action;
                List<Action<object, IEntityManager>> handlers;

                if (!_messageQueue.TryDequeue(out action) ||
                    !_handlers.TryGetValue(action.GetType(), out handlers)) continue;

                foreach (var handler in handlers)
                {
                    handler(action, _entityManager);
                }
            }
        }

        public void RegisterForAction<TAction>(Action<TAction, IEntityManager> handler)
        {
            if (_handlerMap.ContainsKey(handler))
            {
                return;
            }

            Action<object, IEntityManager> wrapper = (x, y) => handler((TAction) x, y);

            _handlerMap[handler] = wrapper;
            List<Action<object, IEntityManager>> handlerList;
            if (!_handlers.TryGetValue(typeof(TAction), out handlerList))
            {
                handlerList = new List<Action<object, IEntityManager>>();
                _handlers[typeof(TAction)] = handlerList;
            }

            handlerList.Add(wrapper);
        }

        public void UnregisterHandler<TAction>(Action<TAction, IEntityManager> handler)
        {
            Action<object, IEntityManager> wrapper;
            if (!_handlerMap.TryGetValue(handler, out wrapper))
            {
                return;
            }

            _handlers[typeof(TAction)].Remove(wrapper);
        }

        public void SendAction<TAction>(TAction action)
        {
            _messageQueue.Enqueue(action);
        }
    }
}