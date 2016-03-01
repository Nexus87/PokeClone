﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.EventComponent
{
    /// <summary>
    /// Represents an event that can be added to the IEventQueue service
    /// </summary>
    /// <remarks>
    /// The Dispatch method is called to start the event. Subclasses must
    /// call OnEventProcessed to signal, when they are done.
    /// </remarks>
    public interface IEvent
    {
        /// <summary>
        /// Signals, that the event is done.
        /// </summary>
        event EventHandler OnEventProcessed;
        /// <summary>
        /// This function is called to "start" the event.
        /// </summary>
        void Dispatch();
    }
}
