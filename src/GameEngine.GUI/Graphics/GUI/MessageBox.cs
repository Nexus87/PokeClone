using System;
using GameEngine.Globals;
using GameEngine.GUI.Graphics.General;
using GameEngine.Registry;
using GameEngine.Utils;

namespace GameEngine.GUI.Graphics.GUI
{
    [GameType]
    public class MessageBox : ForwardingGraphicComponent<ITextGraphicContainer>, IWidget
    {
        private readonly ITextGraphicContainer _textBox;

        public MessageBox(ISpriteFont font, ITextSplitter splitter, int lineNumber = 2)
            : this(new MultlineTextBox(font, splitter, lineNumber))
        {
        }

        public MessageBox(ITextGraphicContainer textBox) :
            base(textBox)
        {
            _textBox = InnerComponent;
        }

        public event EventHandler OnAllLineShowed = delegate { };

        public void DisplayText(string text)
        {
            _textBox.Text = text;
        }

        public override void HandleKeyInput(CommandKeys key)
        {
            if (key != CommandKeys.Select)
                return;

            if (_textBox.HasNext())
                _textBox.NextLine();
            else
                OnAllLineShowed(this, null);

        }

        public void ResetText()
        {
            _textBox.Text = "";
        }

        protected override void Update()
        {
        }
    }
}