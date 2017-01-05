using GameEngine.Graphics.General;
using GameEngine.GUI.Renderers;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Controlls
{
    [GameType]
    public class Label : AbstractGuiComponent
    {
        private readonly LabelRenderer _renderer;
        private string _text;
        private float _textHeight = 32;

        public Label(LabelRenderer renderer)
        {
            IsSelectable = false;
            _renderer = renderer;
            Text = null;
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value ?? "";
                Invalidate();
            }
        }

        public float TextHeight
        {
            get { return _textHeight; }
            set
            {
                _textHeight = value;
                Invalidate();
            }
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _renderer.Render(batch, this);
        }

        protected override void Update()
        {
            PreferredHeight = _renderer.GetPreferedHeight(this);
            PreferredWidth = _renderer.GetPreferedWidth(this);
        }
    }
}