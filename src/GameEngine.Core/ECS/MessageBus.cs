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

            void Wrapper(object x, IEntityManager y) => handler((TAction) x, y);

            _handlerMap[handler] = Wrapper;
            if (!_handlers.TryGetValue(typeof(TAction), out var handlerList))
            {
                handlerList = new List<Action<object, IEntityManager>>();
                _handlers[typeof(TAction)] = handlerList;
            }

            handlerList.Add(Wrapper);
        }

        public void UnregisterHandler<TAction>(Action<TAction, IEntityManager> handler)
        {
            if (!_handlerMap.TryGetValue(handler, out var wrapper))
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

                if (_handlers.TryGetValue(action.GetType(), out var handlers))
                    handlers.ForEach(x => x(action, _entityManager));
            }
        }

        public void SendAction<TAction>(TAction action)
        {
            _messageQueue.Enqueue(action);
        }

        public void RegisterForAction<TAction>(Action<IEntityManager> handler)
        {
            RegisterForAction<TAction>((a, e) => handler(e));
        }

        public void RegisterForAction<TAction>(Action<TAction> handler)
        {
            RegisterForAction<TAction>((a, e) => handler(a));
        }

        public void RegisterForAction<TAction>(Action handler)
        {
            RegisterForAction<TAction>((a, e) => handler());
        }
    }
}