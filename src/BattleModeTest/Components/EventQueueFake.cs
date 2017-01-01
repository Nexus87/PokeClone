using System.Collections.Generic;
using GameEngine.Core.GameEngineComponents;
using Microsoft.Xna.Framework;

namespace BattleModeTest.Components
{
    public class EventQueueFake : IEventQueue
    {
        private readonly List<IEvent> _events = new List<IEvent>();
        public void DispatchAllEvents()
        {
            foreach (var ev in _events)
                ev.Dispatch();

            _events.Clear();
        }
        public void AddEvent(IEvent newEvent)
        {
            _events.Add(newEvent);
        }

        public void Update(GameTime time)
        {
        }
    }
}