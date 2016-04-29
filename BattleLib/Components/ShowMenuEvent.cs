using BattleLib.GraphicComponents;
using GameEngine;
using System;

namespace BattleLib.Components
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
