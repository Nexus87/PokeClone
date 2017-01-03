using System;
using BattleMode.Gui;
using GameEngine.Entities;

namespace BattleMode.Core.Components
{
    public class ShowMenuEvent : IEvent
    {
        public event EventHandler EventProcessed = delegate { };
        private readonly IGUIService _service;

        public ShowMenuEvent(IGUIService service)
        {
            _service = service;
        }

        public void Dispatch()
        {
            _service.MenuShowed += MenuShowedHandler;
            _service.ShowMenu();
        }

        private void MenuShowedHandler(object sender, EventArgs e)
        {
            _service.MenuShowed -= MenuShowedHandler;
            EventProcessed(this, EventArgs.Empty);
        }
    }
}
