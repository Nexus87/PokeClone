using System;
using BattleLib.Components.GraphicComponents;
using GameEngine.GameEngineComponents;

namespace BattleLib.Components
{
    public class ShowMenuEvent : IEvent
    {
        public event EventHandler EventProcessed = delegate { };
        private readonly IGUIService service;

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
            EventProcessed(this, EventArgs.Empty);
        }
    }
}
