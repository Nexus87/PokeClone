using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.EventComponent
{
    class EventQueue : IGameComponent, IEventQueue
    {
        bool eventDispatched = false;
        IEvent dispatchedEvent;

        LinkedList<IEvent> eventQueue = new LinkedList<IEvent>();

        public void Update(GameTime gameTime)
        {
            if (eventDispatched || eventQueue.Count == 0)
                return;

            var ev = eventQueue.First.Value;
            eventQueue.RemoveFirst();
            ev.OnEventProcessed += OnEventProcessedHandler;

            eventDispatched = true;
            dispatchedEvent = ev;
            ev.Dispatch();
        }

        private void OnEventProcessedHandler(Object sender, EventArgs args)
        {
            dispatchedEvent.OnEventProcessed -= OnEventProcessedHandler;
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
