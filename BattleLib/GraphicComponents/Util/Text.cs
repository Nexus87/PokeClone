using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.GraphicComponents.Util
{
    class TextGraphic
    {
        public float X { get { return position.X; } set { position.X = value; } }
        public float Y { get { return position.Y; } set { position.Y = value; } }

        public float Width { get; set; }
        public float Height { get; set; }

        Vector2 scale;
        Vector2 position;
        Vector2 textScale;

        string fontName;
        SpriteFont font;
        public String Text { get; set; }

        public TextGraphic(string fontName)
        {
            this.fontName = fontName;
            Text = "";
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

            var textSize = font.MeasureString(Text);

            textScale.X = 1.0f / textSize.X;
            textScale.Y = 1.0f / textSize.Y;

            RecalculateScale();
        }

        public void Draw(SpriteBatch batch)
        {
            position.X = 0.05f;
            position.Y = 0.05f;
            batch.DrawString(font, Text, position, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        private void RecalculateScale()
        {
            scale.X = Width.CompareTo(0) <= 0 ? 1.0f * textScale.X : Width * textScale.X;
            scale.Y = Height.CompareTo(0) <= 0 ? 1.0f * textScale.Y : Height * textScale.Y;
        }
    }
}
