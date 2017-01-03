using System;
using System.Collections.Generic;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.Entities
{
    [GameService(typeof(IEventQueue))]
    internal class EventQueue : IEventQueue
    {
        private bool _eventDispatched;
        private IEvent _dispatchedEvent;

        private readonly LinkedList<IEvent> _eventQueue = new LinkedList<IEvent>();

        public void Update(GameTime gameTime)
        {
            if (_eventDispatched || _eventQueue.Count == 0)
                return;

            var ev = _eventQueue.First.Value;
            _eventQueue.RemoveFirst();
            ev.EventProcessed += OnEventProcessedHandler;

            _eventDispatched = true;
            _dispatchedEvent = ev;
            ev.Dispatch();
        }

        private void OnEventProcessedHandler(object sender, EventArgs args)
        {
            _dispatchedEvent.EventProcessed -= OnEventProcessedHandler;
            _eventDispatched = false;
        }

        public void AddEvent(IEvent ev)
        {
            _eventQueue.AddLast(ev);
        }
    }
}
