using System;
using BattleMode.Gui;
using GameEngine.Entities;

namespace BattleMode.Core.Components
{
    public class ShowMenuEvent : IEvent
    {
        public event EventHandler EventProcessed = delegate { };
        private readonly IGuiController _controller;

        public ShowMenuEvent(IGuiController controller)
        {
            _controller = controller;
        }

        public void Dispatch()
        {
            _controller.MenuShowed += MenuShowedHandler;
            _controller.ShowMenu();
        }

        private void MenuShowedHandler(object sender, EventArgs e)
        {
            _controller.MenuShowed -= MenuShowedHandler;
            EventProcessed(this, EventArgs.Empty);
        }
    }
}
