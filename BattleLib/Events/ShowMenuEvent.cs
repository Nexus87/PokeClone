using BattleLib.GraphicComponents;
using GameEngine.EventComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Events
{
    public class ShowMenuEvent : IEvent
    {
        public event EventHandler OnEventProcessed = delegate { };
        private IGUIService service;

        public ShowMenuEvent(IGUIService service)
        {
            this.service = service;
        }

        public void Dispatch()
        {
            service.MenuShowed += MenuShowedHandler;
            service.ShowMenu();
        }

        private void MenuShowedHandler(object sender, EventArgs e)
        {
            service.MenuShowed -= MenuShowedHandler;
            OnEventProcessed(this, EventArgs.Empty);
        }
    }
}
