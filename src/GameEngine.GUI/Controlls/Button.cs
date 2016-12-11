using System;
using GameEngine.Globals;
using GameEngine.Graphics;
using GameEngine.Graphics.General;
using GameEngine.GUI.Renderers;
using GameEngine.Utils;
using Microsoft.Xna.Framework;

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

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _buttonRenderer.Render(batch, this);
        }

        public override void Setup()
        {
            base.Setup();
            UpdatePreferredSize();
        }

        private void UpdatePreferredSize()
        {
            PreferredHeight = _buttonRenderer.GetPreferedHeight(this);
            PreferredWidth = _buttonRenderer.GetPreferedWidth(this);
        }
    }
}