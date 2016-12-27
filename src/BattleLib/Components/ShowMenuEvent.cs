using System;
using BattleMode.Core.Components.GraphicComponents;
using BattleMode.Gui;
using GameEngine.Core.GameEngineComponents;

namespace BattleMode.Core.Components
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
