using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Graphics.Basic
{
    public class TextBox : IGraphicComponent
    {
        private float height;

        private bool needsUpdate = true;

        private string text = "";

        private TextGraphic textGraphic;

        private float width;

        public TextBox(String fontName)
        {
            textGraphic = new TextGraphic(fontName);
        }

        public event EventHandler<EventArgs> PositionChanged = (a, b) => { };

        public event EventHandler<EventArgs> SizeChanged = (a, b) => { };

        public float Height { get { return height; } set { height = value; SizeChanged(this, null); } }
        public string Text { get { return text; } set { text = value; Invalidate(); } }
        public float TextSize { get { return textGraphic.TextSize; } set { textGraphic.TextSize = value; Invalidate(); } }

        public float Width { get { return width; } set { width = value; Invalidate(); SizeChanged(this, null); } }
        public float X { get { return textGraphic.X; } set { textGraphic.X = value; PositionChanged(this, null); } }
        public float Y { get { return textGraphic.Y; } set { textGraphic.Y = value; PositionChanged(this, null); } }

        public int DisplayableChars()
        {
            float charWidth = textGraphic.GetSingleCharWidth();
            if (charWidth.CompareTo(0) == 0)
                throw new InvalidOperationException("Textbox is not set up");

            int num = (int)Math.Floor(Width / charWidth);
            return num;
        }

        public void Draw(GameTime time, SpriteBatch batch)
        {
            if (needsUpdate)
                CalculateDisplayedChars();

            textGraphic.Draw(batch);
        }

        public void Setup(ContentManager content)
        {
            textGraphic.Setup(content);
        }

        private void CalculateDisplayedChars()
        {
            float length = textGraphic.CalculateTextLength(" ");
            if (length.CompareTo(0) == 0)
            {
                textGraphic.Text = "";
                return;
            }

            int cnt = (int)Math.Floor(width / length);
            textGraphic.Text = text.Substring(0, Math.Min(text.Length, cnt));

            needsUpdate = false;
        }

        private void Invalidate()
        {
            needsUpdate = true;
        }
    }
}