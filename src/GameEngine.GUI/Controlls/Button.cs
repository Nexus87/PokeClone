using System;
using GameEngine.GUI.Renderers;

namespace GameEngine.GUI.Controlls
{
    public sealed class Button : GameEngine.GUI.AbstractGraphicComponent
    {
        private readonly IButtonRenderer _buttonRenderer;

        public string Text
        {
            get { return _buttonRenderer.Text; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _buttonRenderer.Text = Text;
                UpdatePreferredSize();
            }
        }

        public float TextHeight
        {
            get { return _buttonRenderer.TextHeight; }
            set
            {
                _buttonRenderer.TextHeight = value;
                UpdatePreferredSize();
            }
        }

        public Button(IButtonRenderer buttonRenderer)
        {
            _buttonRenderer = buttonRenderer;
            Renderer = buttonRenderer;
        }

        public event EventHandler ButtonPressed;

        public override bool IsSelected
        {
            get { return _buttonRenderer.IsSelected; }
            set { _buttonRenderer.IsSelected = value; }
        }

        public override void HandleKeyInput(CommandKeys key)
        {
            OnButtonPressed();
        }

        private void OnButtonPressed()
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
            PreferedHeight = _buttonRenderer.PreferedHeight;
            PreferedWidth = _buttonRenderer.PreferedWidth;
        }
    }
}