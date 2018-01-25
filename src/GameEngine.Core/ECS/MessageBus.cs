using System;
using System.Collections.Generic;
using GameEngine.Core.ECS.Actions;

namespace GameEngine.Core.ECS
{
    public class MessageBus : IMessageBus
    {
        public int ActionCount => _messageQueue.Count;

        private readonly IEntityManager _entityManager;
        private readonly Queue<object> _messageQueue = new Queue<object>();

        private readonly Dictionary<Type, List<Action<object, IEntityManager, IMessageBus>>> _handlers = 
            new Dictionary<Type, List<Action<object, IEntityManager, IMessageBus>>>();

        private readonly Dictionary<object, Action<object, IEntityManager, IMessageBus>> _handlerMap =
            new Dictionary<object, Action<object, IEntityManager, IMessageBus>>();


        public MessageBus(IEntityManager entityManager)
        {
            _entityManager = entityManager;
        }


        public void RegisterForAction<TAction>(Action<TAction, IEntityManager, IMessageBus> handler)
        {
            if (_handlerMap.ContainsKey(handler))
            {
                return;
            }

            void Wrapper(object x, IEntityManager y, IMessageBus z) => handler((TAction) x, y, z);

            _handlerMap[handler] = Wrapper;
            if (!_handlers.TryGetValue(typeof(TAction), out var handlerList))
            {
                handlerList = new List<Action<object, IEntityManager, IMessageBus>>();
                _handlers[typeof(TAction)] = handlerList;
            }

            handlerList.Add(Wrapper);
        }
        private void UnregisterHandler<TAction>(object handler)
        {
            if (!_handlerMap.TryGetValue(handler, out var wrapper))
            {
                return;
            }

            _handlers[typeof(TAction)].Remove(wrapper);
        }
        public void UnregisterHandler<TAction>(Action<TAction, IEntityManager, IMessageBus> handler) => UnregisterHandler<TAction>(handler);
        public void UnregisterHandler<TAction>(Action<IEntityManager, IMessageBus> handler) => UnregisterHandler<TAction>(handler);

        public void UnregisterHandler<TAction>(Action<TAction, IMessageBus> handler) => UnregisterHandler<TAction>(handler);

        public void UnregisterHandler<TAction>(Action<IMessageBus> handler) => UnregisterHandler<TAction>(handler);
        public void UnregisterHandler<TAction>(Action handler) => UnregisterHandler<TAction>(handler);
        public void StartProcess()
        {
            while (_messageQueue.Count > 0)
            {
                var action = _messageQueue.Dequeue();
                if(action.GetType() != typeof(TimeAction))
                {
                    System.Console.WriteLine($"Processing Action of Type: {action.GetType().Name}");
                }
                
                if (_handlers.TryGetValue(action.GetType(), out var handlers))
                    handlers.ForEach(x => x(action, _entityManager, this));
            }
        }

        public void SendAction<TAction>(TAction action)
        {
            _messageQueue.Enqueue(action);
        }
        public void SendAction(object action)
        {
            _messageQueue.Enqueue(action);
        }

        public void RegisterForAction<TAction>(Action<IEntityManager, IMessageBus> handler)
        {
            RegisterForAction<TAction>((a, e, m) => handler(e, m));
        }

        public void RegisterForAction<TAction>(Action<TAction, IMessageBus> handler)
        {
            RegisterForAction<TAction>((a, e, m) => handler(a, m));
        }

        public void RegisterForAction<TAction>(Action<IMessageBus> handler)
        {
            RegisterForAction<TAction>((a, e, m) => handler(m));
        }

        public void RegisterForAction<TAction>(Action handler)
        {
            RegisterForAction<TAction>((a, e, m) => handler());
        }


    }
}