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
    public class TextBox : AbstractGraphicComponent
    {
        string fontName;
        SpriteFont font;

        Vector2 textScale;
        string text;

        public String Text { get { return text; } set { text = value; CalculateFontScale(); } }

        public TextBox(string fontName)
        {
            this.fontName = fontName;
            Text = "";
        }

        public override void Setup(ContentManager content)
        {
            font = content.Load<SpriteFont>(fontName);
            CalculateFontScale();
        }

        private void CalculateFontScale()
        {
            if (font == null)
                return;

            var textSize = font.MeasureString(text);

            textScale.X = 1.0f / textSize.X;
            textScale.Y = 1.0f / textSize.Y;

            CalculateScale();
        }

        public override void Draw(GameTime time, SpriteBatch batch)
        {
            position.X = 0.05f;
            position.Y = 0.05f;
            batch.DrawString(font, Text, position, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        protected override void CalculateScale()
        {
            scale.X = Width.CompareTo(0) <= 0 ? 1.0f * textScale.X * textScale.X : Width * textScale.X;
            scale.Y = Height.CompareTo(0) <= 0 ? 1.0f * textScale.Y * textScale.Y : Height * textScale.Y;
        }
    }
}
