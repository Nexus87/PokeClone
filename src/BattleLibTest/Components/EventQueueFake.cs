using System.Collections.Generic;
using GameEngine;
using Microsoft.Xna.Framework;

namespace BattleLibTest.Components
{
    public class EventQueueFake : IEventQueue
    {
        private List<IEvent> events = new List<IEvent>();
        public void DispatchAllEvents()
        {
            foreach (var ev in events)
                ev.Dispatch();

            events.Clear();
        }
        public void AddEvent(IEvent newEvent)
        {
            events.Add(newEvent);
        }

        public void Initialize()
        {
        }

        public void Update(GameTime time)
        {
        }
    }
}