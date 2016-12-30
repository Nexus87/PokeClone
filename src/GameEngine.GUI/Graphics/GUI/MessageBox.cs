using System;
using GameEngine.Globals;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Renderers;
using GameEngine.GUI.Utils;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics.GUI
{
    [GameType]
    public class MessageBox : AbstractGraphicComponent
    {
        private readonly ITextGraphicContainer _textBox;

        public MessageBox(ITextAreaRenderer renderer, ITextSplitter splitter)
            : this(new TextArea(renderer, splitter))
        {
        }

        internal MessageBox(ITextGraphicContainer textBox)
        {
            _textBox = textBox;
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

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _textBox.Draw(time, batch);
        }

        public void ResetText()
        {
            _textBox.Text = "";
        }

        protected override void Update()
        {
            _textBox.SetCoordinates(this);
        }

        public override void Setup()
        {
            _textBox.Setup();
        }
    }
}