using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Graphics.Basic
{
    public class TextGraphic
    {
        private ISpriteFont font;
        private string fontName;
        private bool needsUpdate = false;
        private Vector2 position;
        private float scale;
        private string text = "";
        private float textSize = 32.0f;

        public TextGraphic(string fontName, ISpriteFont font)
        {
            this.fontName = fontName;
            this.font = font;
        }

        public String Text { get { return text; } set { text = value; Invalidate(); } }
        public float TextSize
        {
            get { return textSize; }
            set
            {
                if (value.CompareTo(0) < 0)
                    throw new ArgumentException("TextSize must be >= 0");
                textSize = value;
                Invalidate();
            }
        }
        public float TextWidth { get { return CalculateTextLength(text); } }
        public float X { get { return position.X; } set { position.X = value; } }
        public float Y { get { return position.Y; } set { position.Y = value; } }

        public float CalculateTextLength(string text)
        {
            if (font == null)
                return 0;

            var size = font.MeasureString(text);
            return textSize * size.X / size.Y;
        }

        public void Draw(ISpriteBatch batch)
        {
            if (needsUpdate)
                Update();
            batch.DrawString(font, Text, position, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public float GetSingleCharWidth()
        {
            return CalculateTextLength(" ");
        }

        public void Setup(ContentManager content)
        {
            font.Load(content, fontName);
        }

        private void Invalidate()
        {
            needsUpdate = true;
        }

        private void Update()
        {
            if (font == null)
                return;

            var size = font.MeasureString(" ");

            scale = textSize / size.Y;
            needsUpdate = false;
        }
    }
}