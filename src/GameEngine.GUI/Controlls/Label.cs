using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Controlls
{
    [GameType]
    public class Label : AbstractGraphicComponent
    {
        private readonly ILabelRenderer _renderer;
        private string _text;
        private float _textSize = 32;

        public Label(ILabelRenderer renderer)
        {
            IsSelectable = false;
            _renderer = renderer;
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                Invalidate();
            }
        }

        public float TextSize
        {
            get { return _textSize; }
            set
            {
                _textSize = value;
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