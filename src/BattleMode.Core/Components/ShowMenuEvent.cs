using System;
using BattleMode.Gui;
using GameEngine.Entities;

namespace BattleMode.Core.Components
{
    public class ShowMenuEvent : IEvent
    {
        public event EventHandler EventProcessed = delegate { };
        private readonly IGuiEntity _entity;

        public ShowMenuEvent(IGuiEntity entity)
        {
            _entity = entity;
        }

        public void Dispatch()
        {
            _entity.MenuShowed += MenuShowedHandler;
            _entity.ShowMenu();
        }

        private void MenuShowedHandler(object sender, EventArgs e)
        {
            _entity.MenuShowed -= MenuShowedHandler;
            EventProcessed(this, EventArgs.Empty);
        }
    }
}
