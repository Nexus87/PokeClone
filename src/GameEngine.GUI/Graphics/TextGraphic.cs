using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using GameEngine.Graphics.General;

namespace GameEngine.Graphics
{
    public class TextGraphic : IGraphicalText
    {
        private ISpriteFont font;
        private bool needsUpdate;
        private Vector2 position;
        private float scale;
        private string text = "";
        private float charHeight = 32.0f;

        public TextGraphic(ISpriteFont font)
        {
            this.font = font;
        }

        public string Text 
        { 
            get { return text; }
            set
            {
                text = value;
                Invalidate();
            } 
        }
        
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
            return CalculateTextLength(testText, CharHeight);
        }

        private float CalculateTextLength(string testText, float charSize)
        {
            if (font == null)
                return 0;

            if (testText == null)
                return 0;
   
            var size = font.MeasureString(testText);
            return charSize * size.X / size.Y;
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

        public float GetSingleCharWidth(float charSize)
        {
            return CalculateTextLength(" ", charSize);
        }

        public void Setup()
        {
            font.LoadContent();
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

            scale = charHeight / size.Y;
            needsUpdate = false;
        }

        public ISpriteFont SpriteFont { get; set; }
    }
}