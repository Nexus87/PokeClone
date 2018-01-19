using System;
using GameEngine.Globals;
using GameEngine.GUI.Renderers;

namespace GameEngine.GUI.Controlls
{
    public sealed class Button : AbstractGuiComponent
    {
        private string _text;
        private float _textHeight;

        public Button()
        {
            TextHeight = 32;
            IsSelectable = true;
        }

        public bool Enabled
        {
            get => IsSelectable;
            set => IsSelectable = value;
        }

        public string Text
        {
            get => _text;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (_text == value) return;
                _text = value;
                Invalidate();
            }
        }

        public float TextHeight
        {
            get => _textHeight;
            set
            {
                if (value.AlmostEqual(_textHeight)) return;
                _textHeight = value;
                Invalidate();
            }
        }

        public event EventHandler ButtonPressed;

        public override void HandleKeyInput(CommandKeys key)
        {
            if (key == CommandKeys.Select)
                OnButtonPressed();
        }

        private void OnButtonPressed()
        {
            ButtonPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}