using GameEngine.Registry;
using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Graphics
{
    internal class ItemBox : AbstractGraphicComponent, ISelectableTextComponent
    {
        public bool IsSelected { get; private set; }
        private IGraphicComponent arrow;
        private ITextGraphicComponent textBox;

        public string Text { get { return textBox.Text; } set { textBox.Text = value; } }

        internal ItemBox(IGraphicComponent arrow, ITextGraphicComponent textBox)
        {
            this.arrow = arrow;
            this.textBox = textBox;
        }

        public ItemBox(ITexture2D arrowTexture, ISpriteFont font) : 
            this(new TextureBox(arrowTexture), new TextBox(font)) { }

        public override void Setup()
        {
            arrow.Setup();
            textBox.Setup();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            if (IsSelected)
                arrow.Draw(time, batch);
            
            textBox.Draw(time, batch);
        }

        protected override void Update()
        {

            textBox.Height = Height;

            // If we can't draw the whole arrow, draw no arrow at all
            float arrowHeight = textBox.RealTextHeight;
            float arrowWidth = Width.CompareTo(arrowHeight) <= 0 ? 0 : arrowHeight;

            arrow.XPosition = XPosition;
            arrow.YPosition = YPosition;
            arrow.Width = arrowWidth;
            arrow.Height = arrowHeight;

            textBox.XPosition = XPosition + arrowWidth;
            textBox.YPosition = YPosition;
            textBox.Width = Math.Max(Width - arrowWidth, 0);

        }

        public void Select()
        {
            IsSelected = true;
        }

        public void Unselect()
        {
            IsSelected = false;
        }

        public int DisplayableChars()
        {
            return textBox.DisplayableChars();
        }

        public float PreferedTextHeight
        {
            get
            {
                return textBox.PreferedTextHeight;
            }
            set
            {
                textBox.PreferedTextHeight = value;
            }
        }

        public float RealTextHeight
        {
            get { return textBox.RealTextHeight; }
        }


        public ISpriteFont SpriteFont
        {
            get
            {
                return textBox.SpriteFont;
            }
            set
            {
                textBox.SpriteFont = value;
            }
        }
    }
}
