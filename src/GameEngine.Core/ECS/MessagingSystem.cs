using System;
using System.Collections.Generic;

namespace GameEngine.Core.ECS
{
    public class MessagingSystem
    {
        private delegate void HandlerWrapper(object o);

        private readonly Dictionary<Type, List<HandlerWrapper>> _handlers = new Dictionary<Type, List<HandlerWrapper>>();
        private readonly Dictionary<object, HandlerWrapper> _originalHandler = new Dictionary<object, HandlerWrapper>();

        public void SendMessage<T>(T message)
        {
            _handlers[typeof(T)].ForEach(x => x(message));
        }

        public void ListenForMessage<T>(Action<T> handler)
        {
            List<HandlerWrapper> listeners;
            if (!_handlers.TryGetValue(typeof(T), out listeners))
            {
                listeners = new List<HandlerWrapper>();
                _handlers[typeof(T)] = listeners;
            }

            HandlerWrapper handlerWrapper = o => handler((T) o);

            listeners.Add(handlerWrapper);
            _originalHandler[handler] = handlerWrapper;
        }

        public void UnregisterForMessage<T>(Action<T> handler)
        {
            var handlerWrapper = _originalHandler[handler];
            _handlers[typeof(T)].Remove(handlerWrapper);
        }
    }
}