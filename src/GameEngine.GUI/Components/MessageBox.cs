using System;
using GameEngine.Globals;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.General;
using GameEngine.GUI.Panels;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Components
{
    [GameType]
    public class MessageBox : AbstractGraphicComponent
    {
        private readonly Window _window;
        private readonly TextArea _textArea;

        public MessageBox(Window window, TextArea textArea)
        {
            _window = window;
            _textArea = textArea;

            _window.SetContent(_textArea);
        }

        public event EventHandler OnAllLineShowed = delegate { };

        public void DisplayText(string text)
        {
            _textArea.Text = text;
        }

        public override void HandleKeyInput(CommandKeys key)
        {
            if (key != CommandKeys.Select)
                return;

            if (_textArea.HasNext())
                _textArea.HandleKeyInput(key);
            else
                OnAllLineShowed(this, null);

        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _window.Draw(time, batch);
        }

        public void ResetText()
        {
            _textArea.Text = "";
        }

        protected override void Update()
        {
            _window.SetCoordinates(this);
        }

        public override void Setup()
        {
            _window.Setup();
        }
    }
}