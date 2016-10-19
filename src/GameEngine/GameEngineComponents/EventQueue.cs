using System;
using System.Collections.Generic;
using GameEngine.Registry;
using Microsoft.Xna.Framework;

namespace GameEngine.GameEngineComponents
{
    [GameService(typeof(IEventQueue))]
    internal class EventQueue : IEventQueue
    {
        private bool eventDispatched;
        private IEvent dispatchedEvent;

        private readonly LinkedList<IEvent> eventQueue = new LinkedList<IEvent>();

        public void Update(GameTime gameTime)
        {
            if (eventDispatched || eventQueue.Count == 0)
                return;

            var ev = eventQueue.First.Value;
            eventQueue.RemoveFirst();
            ev.EventProcessed += OnEventProcessedHandler;

            eventDispatched = true;
            dispatchedEvent = ev;
            ev.Dispatch();
        }

        private void OnEventProcessedHandler(object sender, EventArgs args)
        {
            dispatchedEvent.EventProcessed -= OnEventProcessedHandler;
            eventDispatched = false;
        }

        public void AddEvent(IEvent ev)
        {
            eventQueue.AddLast(ev);
        }

        public void Initialize()
        {
        }
    }
}
