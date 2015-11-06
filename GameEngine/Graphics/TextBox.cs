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
    public class GraphicText
    {
        private static EventHandler AspectRationChanged;

        internal static float AspectRation
        {
            get { return aspectRation; }
            set {   
                aspectRation = value;
                if (AspectRationChanged != null)
                    AspectRationChanged(null, null);
            }
        }
        private static float aspectRation = 1.0f;

        public float X { get { return position.X; } set { position.X = value; } }
        public float Y { get { return position.Y; } set { position.Y = value; } }

        public float TextSize { get { return textSize; } set { textSize = value; CalculateFontScale(); } }
        public String Text { get { return text; } set { text = value; CalculateFontScale(); } }

        string fontName;
        SpriteFont font;
        
        string text;

        float textSize;
        Vector2 scale;
        Vector2 position;


        public GraphicText(string fontName)
        {
            this.fontName = fontName;
            text = "";
            AspectRationChanged += OnAspectRationChanged;
        }

        private void OnAspectRationChanged(object sender, EventArgs e)
        {
            CalculateFontScale();
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

            var size = font.MeasureString(text);

            float quotient = size.X / size.Y;

            scale.X = aspectRation * textSize / size.Y;
            scale.Y = textSize / size.Y;

        }

        public void Draw(SpriteBatch batch)
        {
            position.X = 0.05f;
            position.Y = 0.05f;
            batch.DrawString(font, Text, position, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}
