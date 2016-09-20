using GameEngine.Registry;
using GameEngine.Utils;
using System;

namespace GameEngine.Graphics.GUI
{
    [GameType]
    public class MessageBox : ForwardingGraphicComponent<ITextGraphicContainer>, IWidget
    {
        private readonly ITextGraphicContainer textBox;

        public MessageBox(ISpriteFont font, ITextSplitter splitter, int lineNumber = 2)
            : this(new MultlineTextBox(font, splitter, lineNumber))
        {
        }

        internal MessageBox(ITextGraphicContainer textBox) :
            base(textBox)
        {
            this.textBox = InnerComponent;
        }

        public event EventHandler OnAllLineShowed = delegate { };

        public void DisplayText(string text)
        {
            textBox.Text = text;
        }

        public bool HandleInput(CommandKeys key)
        {
            if (key != CommandKeys.Select)
                return false;

            if (textBox.HasNext())
                textBox.NextLine();
            else
                OnAllLineShowed(this, null);

            return true;
        }

        public void ResetText()
        {
            textBox.Text = "";
        }

        protected override void Update()
        {
        }
    }
}