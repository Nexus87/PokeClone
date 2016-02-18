using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.EventComponent
{
    public interface IEvent
    {
        event EventHandler OnEventProcessed;
        void Dispatch();
    }
}
