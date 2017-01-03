using System;
using BattleMode.Gui;
using GameEngine.Entities;

namespace BattleMode.Core.Components
{
    public class ShowMessageEvent : IEvent
    {
        public event EventHandler EventProcessed = delegate { };
        private readonly IGuiEntity _guiEntity;
        private readonly string _text;

        public ShowMessageEvent(IGuiEntity guiEntity, string text)
        {
            _guiEntity = guiEntity;
            _text = text;
        }
        public void Dispatch()
        {
            _guiEntity.TextDisplayed += TextDisplayedHandler;
            _guiEntity.SetText(_text);
        }

        private void TextDisplayedHandler(object sender, EventArgs e)
        {
            _guiEntity.TextDisplayed -= TextDisplayedHandler;
            EventProcessed(this, EventArgs.Empty);
        }
    }
}
