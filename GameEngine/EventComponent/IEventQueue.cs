﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.EventComponent
{
    /// <summary>
    /// Event queue service.
    /// </summary>
    /// <remarks>
    /// This is a fire and forget event queue. Implementations are not required
    /// to report back the result of the event.
    /// </remarks>
    public interface IEventQueue
    {
        /// <summary>
        /// Add a new event to the event queue
        /// </summary>
        /// <param name="ev">Event</param>
        void AddEvent(IEvent ev);
    }
}
