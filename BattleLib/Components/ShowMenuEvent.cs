using BattleLib.GraphicComponents;
using GameEngine;
using System;

namespace BattleLib.Components
{
    public class ShowMenuEvent : IEvent
    {
        public event EventHandler EventProcessed = delegate { };
        readonly IGUIService service;

        public ShowMenuEvent(IGUIService service)
        {
            this.service = service;
        }

        public void Dispatch()
        {
            service.MenuShowed += MenuShowedHandler;
            service.ShowMenu();
        }

        void MenuShowedHandler(object sender, EventArgs e)
        {
            service.MenuShowed -= MenuShowedHandler;
            EventProcessed(this, EventArgs.Empty);
        }
    }
}
