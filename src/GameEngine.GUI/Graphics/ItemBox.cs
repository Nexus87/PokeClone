using GameEngine.GUI.Graphics.General;

namespace GameEngine.GUI.Graphics
{
    public class ItemBox : SelectableContainer<ITextGraphicComponent>, ISelectableTextComponent
    {
        private readonly ITextGraphicComponent _textBox;

        public string Text { get { return _textBox.Text; } set { _textBox.Text = value; } }

        public ItemBox(IGraphicComponent arrow, ITextGraphicComponent textBox) :
            base(arrow, textBox)
        {
            this._textBox = textBox;

            VerticalPolicy = ResizePolicy.Preferred;
        }

        public ItemBox(ITexture2D arrowTexture, ISpriteFont font) : 
            this(new TextureBox(arrowTexture), new TextBox(font)) { }

        public int DisplayableChars()
        {
            return _textBox.DisplayableChars();
        }

        public float PreferredTextHeight
        {
            get
            {
                return _textBox.PreferredTextHeight;
            }
            set
            {
                _textBox.PreferredTextHeight = value;
            }
        }

        public float RealTextHeight
        {
            get { return _textBox.RealTextHeight; }
        }


        public ISpriteFont SpriteFont
        {
            get
            {
                return _textBox.SpriteFont;
            }
            set
            {
                _textBox.SpriteFont = value;
            }
        }
    }
}
