using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.EventComponent
{
    class EventQueue : GameComponent, IEventQueue
    {
                bool eventDispatched = false;

        public EventQueue(Game game) : base(game) 
        {
            game.Services.AddService(typeof(IEventQueue), this);
        }

        LinkedList<IEvent> eventQueue = new LinkedList<IEvent>();

        public override void Update(GameTime gameTime)
        {
            if (eventDispatched || eventQueue.Count == 0)
                return;

            var ev = eventQueue.First.Value;
            eventQueue.RemoveFirst();
            ev.OnEventProcessed += OnEventProcessedHandler;

            eventDispatched = true;
            ev.Dispatch();
        }

        private void OnEventProcessedHandler(Object sender, EventArgs args)
        {
            var ev = (IEvent)sender;
            ev.OnEventProcessed -= OnEventProcessedHandler;
            eventDispatched = false;
        }

        public void AddEvent(IEvent ev)
        {
            eventQueue.AddLast(ev);
        }

    }
}
