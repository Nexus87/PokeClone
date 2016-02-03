using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace GameEngine.Graphics.Basic
{
    public class TextBox : AbstractGraphicComponent
    {
        private string text = "";
        private TextGraphic textGraphic;
        private float prefTextSize;

        public TextBox(String fontName, Game game) : this(fontName, new XNASpriteFont(), game) { }

        public TextBox(String fontName, ISpriteFont font, Game game) : base(game)
        {
            textGraphic = new TextGraphic(fontName, font);
            prefTextSize = textGraphic.TextSize;
        }

        public float PreferedTextSize
        { 
            get { return prefTextSize; } 
            set 
            {
                if (value.CompareTo(0) < 0)
                    throw new ArgumentException("PreferedTextSize must be >= 0");
                if (prefTextSize == value)
                    return;
                
                prefTextSize = value;
                Invalidate();
            } 
        }

        public string Text { get { return text; } set { text = value; Invalidate(); } }
        public float RealTextHeight { get { return prefTextSize <= Height ? prefTextSize : Height; } }
        
        public int DisplayableChars()
        {
            textGraphic.TextSize = RealTextHeight;
            float charWidth = textGraphic.GetSingleCharWidth();
            if (charWidth.CompareTo(0) == 0)
                return 0;

            int num = (int)Math.Floor(Width / charWidth);
            return num;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            textGraphic.Draw(batch);
        }

        public override void Setup(ContentManager content)
        {
            textGraphic.Setup(content);
        }

        protected override void Update()
        {
            textGraphic.X = X;
            textGraphic.Y = Y;
            textGraphic.TextSize = RealTextHeight;

            float length = textGraphic.CalculateTextLength(" ");
            if (length.CompareTo(0) == 0)
            {
                textGraphic.Text = "";
                return;
            }

            int cnt = (int)Math.Floor(Width / length);
            textGraphic.Text = text.Substring(0, Math.Min(text.Length, cnt));
        }
    }
}