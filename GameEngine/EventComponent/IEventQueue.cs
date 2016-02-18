using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.EventComponent
{
    public interface IEventQueue
    {
        void AddEvent(IEvent ev);
    }
}
