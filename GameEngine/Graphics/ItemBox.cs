namespace GameEngine.Graphics
{
    internal class ItemBox : SelectableContainer<ITextGraphicComponent>, ISelectableTextComponent
    {
        private readonly ITextGraphicComponent textBox;

        public string Text { get { return textBox.Text; } set { textBox.Text = value; } }

        internal ItemBox(IGraphicComponent arrow, ITextGraphicComponent textBox) :
            base(arrow, textBox)
        {
            this.textBox = textBox;

            VerticalPolicy = ResizePolicy.Preferred;
        }

        public ItemBox(ITexture2D arrowTexture, ISpriteFont font) : 
            this(new TextureBox(arrowTexture), new TextBox(font)) { }

        public int DisplayableChars()
        {
            return textBox.DisplayableChars();
        }

        public float PreferredTextHeight
        {
            get
            {
                return textBox.PreferredTextHeight;
            }
            set
            {
                textBox.PreferredTextHeight = value;
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
