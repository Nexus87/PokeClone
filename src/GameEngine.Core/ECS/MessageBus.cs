using System;
using System.Collections.Generic;

namespace GameEngine.Core.ECS
{
    public class MessageBus : IMessageBus
    {
        public int ActionCount => _messageQueue.Count;

        private readonly IEntityManager _entityManager;
        private readonly Queue<object> _messageQueue = new Queue<object>();

        private readonly Dictionary<Type, List<Action<object, IEntityManager>>> _handlers = 
            new Dictionary<Type, List<Action<object, IEntityManager>>>();

        private readonly Dictionary<object, Action<object, IEntityManager>> _handlerMap =
            new Dictionary<object, Action<object, IEntityManager>>();


        public MessageBus(IEntityManager entityManager)
        {
            _entityManager = entityManager;
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

        public void StartProcess()
        {
            while (_messageQueue.Count > 0)
            {
                var action = _messageQueue.Dequeue();
                List<Action<object, IEntityManager>> handlers;

                if (_handlers.TryGetValue(action.GetType(), out handlers))
                    handlers.ForEach(x => x(action, _entityManager));
            }
        }

        public void SendAction<TAction>(TAction action)
        {
            _messageQueue.Enqueue(action);
        }
    }
}