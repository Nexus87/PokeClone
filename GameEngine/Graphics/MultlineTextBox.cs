using GameEngine.Utils;
using Microsoft.Xna.Framework;

namespace GameEngine.Graphics
{
    public class MultlineTextBox : AbstractGraphicComponent
    {
        private string text;
        private ITextSplitter splitter;
        private int currentLineIndex = 0;
        private Container container;
        private int lineNumber;
        private ISpriteFont font;


        private ITextGraphicComponent GetComponent(int index)
        {
            return (ITextGraphicComponent) container.Components[index];
        }

        public MultlineTextBox(ISpriteFont font, ITextSplitter splitter, int lineNumber)
        {
            this.lineNumber = lineNumber;
            this.font = font;
            this.splitter = splitter;
            container = new Container();
            container.Layout = new VBoxLayout();
        }

        public string Text {
            set
            {
                text = value;
                currentLineIndex = 0;
                Invalidate();
            }
            get
            {
                return text;
            }
        
        }

        public bool HasNext()
        {
            if (NeedsUpdate)
            {
                Update();
                NeedsUpdate = false;
            }
            return currentLineIndex + lineNumber < splitter.Count;
        }

        public void NextLine()
        {
            if (!HasNext())
                return;

            currentLineIndex += lineNumber;
            Invalidate();
        }

        protected override void Update()
        {
            UpdateContainer();
            SplitText();
            UpdateTextComponents();
        }

        private void UpdateContainer()
        {
            container.SetCoordinates(this);
            // Layout the container now, otherwise DisplayableChars might return a wrong value
            container.ForceLayout();
        }

        private void UpdateTextComponents()
        {
            for (int i = 0; i < lineNumber; i++)
            {
                var line = splitter.GetString(i + currentLineIndex);
                GetComponent(i).Text = line;
            }
        }

        private void SplitText()
        {
            splitter.SplitText(GetComponent(0).DisplayableChars(), text);
        }

        protected virtual ITextGraphicComponent CreateTextComponent(ISpriteFont font)
        {
            return new TextBox(font);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            container.Draw(time, batch);
        }

        public override void Setup()
        {
            for (int i = 0; i < lineNumber; i++)
                container.AddComponent(CreateTextComponent(font));

            container.Setup();
        }
    }
}