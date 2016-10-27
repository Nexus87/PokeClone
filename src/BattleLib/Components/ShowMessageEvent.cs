using System;
using BattleLib.Components.GraphicComponents;
using GameEngine.GameEngineComponents;

namespace BattleLib.Components
{
    public class ShowMessageEvent : IEvent
    {
        public event EventHandler EventProcessed = delegate { };
        private readonly IGUIService guiService;
        private readonly string text;

        public ShowMessageEvent(IGUIService guiService, string text)
        {
            this.guiService = guiService;
            this.text = text;
        }
        public void Dispatch()
        {
            guiService.TextDisplayed += TextDisplayedHandler;
            guiService.SetText(text);
        }

        private void TextDisplayedHandler(object sender, EventArgs e)
        {
            guiService.TextDisplayed -= TextDisplayedHandler;
            EventProcessed(this, EventArgs.Empty);
        }
    }
}
