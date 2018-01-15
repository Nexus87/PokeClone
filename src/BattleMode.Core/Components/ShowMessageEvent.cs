//using System;
//using BattleMode.Gui;
//using GameEngine.Entities;

//namespace BattleMode.Core.Components
//{
//    public class ShowMessageEvent : IEvent
//    {
//        public event EventHandler EventProcessed = delegate { };
//        private readonly IGuiController _guiController;
//        private readonly string _text;

//        public ShowMessageEvent(IGuiController guiController, string text)
//        {
//            _guiController = guiController;
//            _text = text;
//        }
//        public void Dispatch()
//        {
//            _guiController.TextDisplayed += TextDisplayedHandler;
//            _guiController.SetText(_text);
//        }

//        private void TextDisplayedHandler(object sender, EventArgs e)
//        {
//            _guiController.TextDisplayed -= TextDisplayedHandler;
//            EventProcessed(this, EventArgs.Empty);
//        }
//    }
//}
