using GameEngine.Registry;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Graphics
{
    [GameType]
    public class TextBox : AbstractGraphicComponent, ITextGraphicComponent
    {
        private string text = "";
        private readonly IGraphicalText textGraphic;
        private float preferedTextSize;

        private bool preferedWidthOutdated = true;
        private bool preferedHeightOutdated = true;
        private float preferedHeight;
        private float preferedWidth;

        public TextBox(ISpriteFont font)
            : this(new TextGraphic(font))
        { }

        public TextBox(IGraphicalText textGraphic)
        {
            this.textGraphic = textGraphic;
            PreferedTextHeight = textGraphic.CharHeight;
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

                preferedHeightOutdated = true;
                preferedTextSize = value;
                Invalidate();
            } 
        }

        public string Text { 
            get
            {
                return text;
            }
            set {
                text = value;
                preferedWidthOutdated = true;
                Invalidate();
            }
        }
        public float RealTextHeight { get { return preferedTextSize <= Height ? preferedTextSize : Height; } }
        
        public int DisplayableChars()
        {
            textGraphic.CharHeight = RealTextHeight;
            var charWidth = textGraphic.GetSingleCharWidth();
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

            var length = textGraphic.CalculateTextLength(" ");
            if (length.CompareTo(0) == 0)
            {
                textGraphic.Text = "";
                return;
            }

            var cnt = (int)Math.Floor(Width / length);
            textGraphic.Text = text.Substring(0, Math.Min(text.Length, cnt));
        }


        public ISpriteFont SpriteFont { get { return textGraphic.SpriteFont; } set { textGraphic.SpriteFont = value; } }

        public override float PreferedHeight
        {
            get
            {
                if (preferedHeightOutdated)
                {
                    preferedHeight = PreferedTextHeight;
                    preferedHeightOutdated = false;
                }

                return preferedHeight;
            }
            set
            {
                preferedHeight = value;
                preferedHeightOutdated = false;
            }
        }

        public override float PreferedWidth
        {
            get{
                if (preferedWidthOutdated)
                {
                    preferedWidth = textGraphic.GetSingleCharWidth(PreferedTextHeight) * text.Length;
                    preferedWidthOutdated = false;
                }

                return preferedWidth;
            }
            set
            {
                preferedWidth = value;
                preferedWidthOutdated = false;
            }
        }
    }
}