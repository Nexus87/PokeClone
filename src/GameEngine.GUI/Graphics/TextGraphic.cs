using System;
using GameEngine.GUI.Graphics.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GUI.Graphics
{
    public class TextGraphic : IGraphicalText
    {
        private readonly ISpriteFont _font;
        private bool _needsUpdate;
        private Vector2 _position;
        private float _scale;
        private string _text = "";
        private float _charHeight = 32.0f;

        public TextGraphic(ISpriteFont font)
        {
            _font = font;
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
        
        public float CharHeight
        {
            get { return _charHeight; }
            set
            {
                if (value.CompareTo(0) < 0)
                    throw new ArgumentException("TextSize must be >= 0");
                _charHeight = value;
                Invalidate();
            }
        }

        public float TextWidth => CalculateTextLength(_text);
        public float XPosition { get { return _position.X; } set { _position.X = value; } }
        public float YPosition { get { return _position.Y; } set { _position.Y = value; } }

        public float CalculateTextLength(string testText)
        {
            return CalculateTextLength(testText, CharHeight);
        }

        private float CalculateTextLength(string testText, float charSize)
        {
            if (_font == null)
                return 0;

            if (testText == null)
                return 0;
   
            var size = _font.MeasureString(testText);
            return charSize * size.X / size.Y;
        }

        public void Draw(ISpriteBatch batch)
        {
            if (_needsUpdate)
                Update();
            batch.DrawString(_font, Text, _position, Color.Black, 0, Vector2.Zero, _scale, SpriteEffects.None, 0);
        }

        public float GetSingleCharWidth()
        {
            return CalculateTextLength(" ");
        }

        public float GetSingleCharWidth(float charSize)
        {
            return CalculateTextLength(" ", charSize);
        }

        public void Setup()
        {
            _font.LoadContent();
        }

        private void Invalidate()
        {
            _needsUpdate = true;
        }

        private void Update()
        {
            if (_font == null)
                return;

            var size = _font.MeasureString(" ");

            _scale = _charHeight / size.Y;
            _needsUpdate = false;
        }

        public ISpriteFont SpriteFont { get; set; }
    }
}