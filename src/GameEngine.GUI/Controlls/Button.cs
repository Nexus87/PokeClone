using System;
using GameEngine.Globals;
using GameEngine.GUI.General;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Renderers;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Controlls
{
    [GameType]
    public sealed class Button : AbstractGraphicComponent
    {
        private readonly IButtonRenderer _buttonRenderer;
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

        public Button(IButtonRenderer buttonRenderer)
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

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _buttonRenderer.Render(batch, this);
        }

        protected override void Update()
        {
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