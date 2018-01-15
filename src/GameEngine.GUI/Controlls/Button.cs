using System;
using GameEngine.Globals;
using GameEngine.GUI.Renderers;

namespace GameEngine.GUI.Controlls
{
    public sealed class Button : AbstractGuiComponent
    {
        private readonly ButtonRenderer _buttonRenderer;
        private string _text;
        private float _textHeight;

        public bool Enabled{get { return IsSelectable; } set { IsSelectable = value; }}
        public string Text
        {
            get { return _text; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if(_text == value) return;
                _text = value;
                Invalidate();
            }
        }

        public float TextHeight
        {
            get { return _textHeight; }
            set
            {
                if(value.AlmostEqual(_textHeight)) return;
                _textHeight = value;
                Invalidate();
            }
        }

        public Button(ButtonRenderer buttonRenderer)
        {
            TextHeight = 32;
            _buttonRenderer = buttonRenderer;
            IsSelectable = true;
        }

        public event EventHandler ButtonPressed;

        public override void HandleKeyInput(CommandKeys key)
        {
            if(key == CommandKeys.Select)
                OnButtonPressed();
        }

        internal void OnButtonPressed()
        {
            ButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        public override void Update()
        {
            if (!NeedsUpdate)
                return;

            base.Update();
            UpdatePreferredSize();
        }

        private void UpdatePreferredSize()
        {
            PreferredHeight = _buttonRenderer.GetPreferedHeight(this);
            PreferredWidth = _buttonRenderer.GetPreferedWidth(this);
        }
    }
}