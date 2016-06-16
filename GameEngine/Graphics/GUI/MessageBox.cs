using GameEngine.Registry;
using GameEngine.Utils;
using System;

namespace GameEngine.Graphics.GUI
{
    [GameComponentAttribute]
    public class MessageBox : ForwardingGraphicComponent<ITextGraphicContainer>, IWidget
    {
        private ITextGraphicContainer textBox;

        private bool isVisible;

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

        public event EventHandler<VisibilityChangedEventArgs> VisibilityChanged = delegate { };

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (value == isVisible)
                    return;

                isVisible = value;
                VisibilityChanged(this, new VisibilityChangedEventArgs(isVisible));
            }
        }

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