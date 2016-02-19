using BattleLib.GraphicComponents;
using GameEngine.EventComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Events
{
    public class ShowMessageEvent : IEvent
    {
        public event EventHandler OnEventProcessed = delegate { };
        private IGUIService guiService;
        private string text;

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
            OnEventProcessed(this, EventArgs.Empty);
        }
    }
}
