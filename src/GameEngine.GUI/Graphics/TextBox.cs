using System;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Utils;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics
{
    [GameType]
    public class TextBox : AbstractGraphicComponent, ITextGraphicComponent
    {
        private string _text = "";
        private readonly IGraphicalText _textGraphic;
        private float _preferredTextSize;

        public TextBox(ISpriteFont font)
            : this(new TextGraphic(font))
        { }

        public TextBox(IGraphicalText textGraphic)
        {
            _textGraphic = textGraphic;
            PreferredTextHeight = textGraphic.CharHeight;
        }

        public float PreferredTextHeight
        { 
            get { return _preferredTextSize; }
            set 
            {
                if (value < 0)
                    throw new ArgumentException("PreferredTextSize must be >= 0");

                if (_preferredTextSize.AlmostEqual(value))
                    return;

                _preferredTextSize = value;
                Invalidate();
            } 
        }

        private void SetPreferredSize()
        {
            PreferredHeight = PreferredTextHeight;
            PreferredWidth = _textGraphic.GetSingleCharWidth(PreferredTextHeight) * _text.Length;
        }

        public string Text { 
            get
            {
                return _text;
            }
            set {
                _text = value;
                Invalidate();
            }
        }
        public float RealTextHeight => _preferredTextSize <= Area.Height ? _preferredTextSize : Area.Height;

        public int DisplayableChars()
        {
            _textGraphic.CharHeight = RealTextHeight;
            var charWidth = _textGraphic.GetSingleCharWidth();
            if (charWidth.CompareTo(0) == 0)
                return 0;

            var num = (int)Math.Floor(Area.Width / charWidth);
            return num;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _textGraphic.Draw(batch);
        }

        public override void Setup()
        {
            _textGraphic.Setup();
        }

        protected override void Update()
        {
            SetPreferredSize();
            _textGraphic.XPosition = Area.X;
            _textGraphic.YPosition = Area.Y;
            _textGraphic.CharHeight = RealTextHeight;

            var length = _textGraphic.CalculateTextLength(" ");
            if (length.CompareTo(0) == 0)
            {
                _textGraphic.Text = "";
                return;
            }

            var cnt = (int)Math.Floor(Area.Width / length);
            _textGraphic.Text = _text.Substring(0, Math.Min(_text.Length, cnt));
        }


        public ISpriteFont SpriteFont { get { return _textGraphic.SpriteFont; } set { _textGraphic.SpriteFont = value; } }
    }
}