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
    public class TextGraphic
    {

        public float X { get { return position.X; } set { position.X = value; } }
        public float Y { get { return position.Y; } set { position.Y = value; } }

        public float TextSize { get { return textSize; } set { textSize = value; CalculateFontScale(); } }
        public String Text { get { return text; } set { text = value; CalculateFontScale(); } }
        public float TextWidth { get { return CalculateTextLength(); } }

        private float CalculateTextLength()
        {
            if (font == null)
                return 0;

            return scale * font.MeasureString(text).X;
        }

        string fontName;
        SpriteFont font;
        
        string text;

        float textSize;
        float scale;
        Vector2 position;

        public float CalculateTextLength(string text)
        {
            if (font == null)
                return 0;

            var size = font.MeasureString(text);
            return textSize * size.X / size.Y; 
        }

        public TextGraphic(string fontName)
        {
            this.fontName = fontName;
            text = "";
            textSize = 32.0f;
        }


        public void Setup(ContentManager content)
        {
            font = content.Load<SpriteFont>(fontName);
            CalculateFontScale();
        }

        private void CalculateFontScale()
        {
            if (font == null)
                return;

            var size = font.MeasureString(" ");

            scale = textSize / size.Y;

        }

        public void Draw(SpriteBatch batch)
        {
            batch.DrawString(font, Text, position, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}
