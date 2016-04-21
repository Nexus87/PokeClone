using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Graphics.Basic
{
    public class TextGraphic : IGraphicalText
    {
        ISpriteFont font;
        bool needsUpdate;
        Vector2 position;
        float scale;
        string text = "";
        float charHeight = 32.0f;

        public TextGraphic(ISpriteFont font)
        {
            this.font = font;
        }

        public string Text { get { return text; } set { text = value; Invalidate(); } }
        public float CharHeight
        {
            get { return charHeight; }
            set
            {
                if (value.CompareTo(0) < 0)
                    throw new ArgumentException("TextSize must be >= 0");
                charHeight = value;
                Invalidate();
            }
        }

        public float TextWidth { get { return CalculateTextLength(text); } }
        public float XPosition { get { return position.X; } set { position.X = value; } }
        public float YPosition { get { return position.Y; } set { position.Y = value; } }

        public float CalculateTextLength(string testText)
        {
            if (font == null)
                return 0;

            if (testText == null)
                return 0;
   
            var size = font.MeasureString(testText);
            return charHeight * size.X / size.Y;
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

        public void Setup()
        {
            font.LoadContent();
        }

        void Invalidate()
        {
            needsUpdate = true;
        }

        void Update()
        {
            if (font == null)
                return;

            var size = font.MeasureString(" ");

            scale = charHeight / size.Y;
            needsUpdate = false;
        }

        public ISpriteFont SpriteFont { get; set; }
    }
}