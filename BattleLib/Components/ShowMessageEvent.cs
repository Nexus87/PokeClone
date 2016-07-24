using BattleLib.GraphicComponents;
using GameEngine;
using System;

namespace BattleLib.Components
{
    public class ShowMessageEvent : IEvent
    {
        public event EventHandler EventProcessed = delegate { };
        readonly IGUIService guiService;
        readonly string text;

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

        void TextDisplayedHandler(object sender, EventArgs e)
        {
            guiService.TextDisplayed -= TextDisplayedHandler;
            EventProcessed(this, EventArgs.Empty);
        }
    }
}
