using System;
using GameEngine.GUI.Renderers;
using GameEngine.Utils;

namespace GameEngine.GUI.Controlls
{
    public sealed class Button : AbstractGraphicComponent
    {
        private readonly IButtonRenderer _buttonRenderer;
        private string _text;
        private float _textHeight;

        public string Text
        {
            get { return _text; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if(_text == value) return;
                _text = value;
                UpdatePreferredSize();
            }
        }

        public float TextHeight
        {
            get { return _textHeight; }
            set
            {
                if(value.AlmostEqual(_textHeight)) return;
                _textHeight = value;
                UpdatePreferredSize();
            }
        }

        public Button(IButtonRenderer buttonRenderer)
        {
            _buttonRenderer = buttonRenderer;
        }

        public event EventHandler ButtonPressed;

        public override bool IsSelected { get; set; }

        public override void HandleKeyInput(CommandKeys key)
        {
            OnButtonPressed();
        }

        internal void OnButtonPressed()
        {
            ButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        public override void Init()
        {
            base.Init();
            UpdatePreferredSize();
        }

        private void UpdatePreferredSize()
        {
            PreferedHeight = _buttonRenderer.GetPreferedHeight(this);
            PreferedWidth = _buttonRenderer.GetPreferedWidth(this);
        }
    }
}