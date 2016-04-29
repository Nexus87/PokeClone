using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Graphics
{
    public class TextBox : AbstractGraphicComponent, ITextGraphicComponent
    {
        private string text = "";
        private readonly IGraphicalText textGraphic;
        private float preferedTextSize;

        public TextBox(IPokeEngine game) : this(game.DefaultFont, game) { }

        public TextBox(ISpriteFont font, IPokeEngine game)
            : this(new TextGraphic(font), game)
        { }

        public TextBox(IGraphicalText textGraphic, IPokeEngine game) :
            base (game)
        {
            this.textGraphic = textGraphic;
            preferedTextSize = textGraphic.CharHeight;
        }

        public float PreferedTextHeight
        { 
            get { return preferedTextSize; } 
            set 
            {
                if (value < 0)
                    throw new ArgumentException("PreferedTextSize must be >= 0");

                if (preferedTextSize.AlmostEqual(value))
                    return;
                
                preferedTextSize = value;
                Invalidate();
            } 
        }

        public string Text { get { return text; } set { text = value; Invalidate(); } }
        public float RealTextHeight { get { return preferedTextSize <= Height ? preferedTextSize : Height; } }
        
        public int DisplayableChars()
        {
            textGraphic.CharHeight = RealTextHeight;
            float charWidth = textGraphic.GetSingleCharWidth();
            if (charWidth.CompareTo(0) == 0)
                return 0;

            var num = (int)Math.Floor(Width / charWidth);
            return num;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            textGraphic.Draw(batch);
        }

        public override void Setup()
        {
            textGraphic.Setup();
        }

        protected override void Update()
        {
            textGraphic.XPosition = XPosition;
            textGraphic.YPosition = YPosition;
            textGraphic.CharHeight = RealTextHeight;

            float length = textGraphic.CalculateTextLength(" ");
            if (length.CompareTo(0) == 0)
            {
                textGraphic.Text = "";
                return;
            }

            var cnt = (int)Math.Floor(Width / length);
            textGraphic.Text = text.Substring(0, Math.Min(text.Length, cnt));
        }


        public ISpriteFont SpriteFont { get { return textGraphic.SpriteFont; } set { textGraphic.SpriteFont = value; } }
    }
}