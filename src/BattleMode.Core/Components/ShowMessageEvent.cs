using System;
using BattleMode.Gui;
using GameEngine.Components;

namespace BattleMode.Core.Components
{
    public class ShowMessageEvent : IEvent
    {
        public event EventHandler EventProcessed = delegate { };
        private readonly IGUIService _guiService;
        private readonly string _text;

        public ShowMessageEvent(IGUIService guiService, string text)
        {
            _guiService = guiService;
            _text = text;
        }
        public void Dispatch()
        {
            _guiService.TextDisplayed += TextDisplayedHandler;
            _guiService.SetText(_text);
        }

        private void TextDisplayedHandler(object sender, EventArgs e)
        {
            _guiService.TextDisplayed -= TextDisplayedHandler;
            EventProcessed(this, EventArgs.Empty);
        }
    }
}
