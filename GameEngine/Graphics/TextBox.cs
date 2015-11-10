using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public class TextBox : IGraphicComponent
    {
        public string Text { get { return text; } set { text = value; CalculateDisplayedChars(); } }
        public float TextSize { get { return textGraphic.TextSize; } set { textGraphic.TextSize = value; CalculateDisplayedChars(); } }

        public float X { get { return x; } set { x = value; textGraphic.X = x; } }
        public float Y { get { return y; } set { y = value; textGraphic.Y = y; } }

        public float Width { get { return width; } set { width = value; CalculateDisplayedChars(); } }
        public float Height { get { return height; } set { height = value; } }

        private TextGraphic textGraphic;
        private float x;
        private float y;

        private float width;
        private float height;

        string text = "";
        public TextBox(String fontName)
        {
            textGraphic = new TextGraphic(fontName);
        }

        public void Draw(GameTime time, SpriteBatch batch)
        {
            textGraphic.Draw(batch);
        }

        public void Setup(ContentManager content)
        {
            textGraphic.Setup(content);
            CalculateDisplayedChars();
        }

        private void CalculateDisplayedChars()
        {
            float length = textGraphic.CalculateTextLength(" ");
            if (length == 0)
            {
                textGraphic.Text = "";
                return;
            }

            int cnt = (int) Math.Floor(width / length);
            textGraphic.Text = text.Substring(0, Math.Min(text.Length, cnt));
        }
    }
}
