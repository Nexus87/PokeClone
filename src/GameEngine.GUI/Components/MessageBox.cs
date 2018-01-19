using System;
using GameEngine.Globals;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Panels;

namespace GameEngine.GUI.Components
{
    public class MessageBox : AbstractGuiComponent
    {
        private readonly Window _window;
        private readonly TextArea _textArea;

        public MessageBox() : this(new Window(), new TextArea()){}
        public MessageBox(Window window, TextArea textArea)
        {
            _window = window;
            _textArea = textArea;

            _window.Content = _textArea;

            Children.Add(_window);
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

        public void ResetText()
        {
            _textArea.Text = "";
        }

        public override void Update()
        {
            _window.SetCoordinates(this);
        }
    }
}