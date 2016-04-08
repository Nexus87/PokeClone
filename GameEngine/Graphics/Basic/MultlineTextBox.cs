using GameEngine.Graphics.Layouts;
using GameEngine.Wrapper;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace GameEngine.Graphics.Basic
{
    public class MultlineTextBox : ForwardingGraphicComponent<Container>
    {
        private readonly List<ITextGraphicComponent> texts = new List<ITextGraphicComponent>();
        private string text;
        private ITextSplitter splitter = new DefaultTextSplitter();
        private int currentLineIndex = 0;

        public MultlineTextBox(PokeEngine game) : this(2, game) 
        { }
        
        public MultlineTextBox(int lineNumber, PokeEngine game)
            : this(game.DefaultFont, lineNumber, game)
        { }

        public MultlineTextBox(ISpriteFont font, int lineNumber, PokeEngine game)
            : base(new Container(game), game)
        {
            font.CheckNull("font");
            
            var container = InnerComponent;
            container.Layout = new VBoxLayout();

            for (int i = 0; i < lineNumber; i++)
            {
                var box = CreateTextComponent(font);
                texts.Add(box);
                container.AddComponent(box);
            }
        }

        public int CharsPerLine()
        {
            // If we don't force the layout to update, DisplayableChars might
            // return 0
            InnerComponent.ForceLayout();
            return texts[0].DisplayableChars();
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
            if (splitter.Count == 0)
                return false;

            return currentLineIndex + texts.Count < splitter.Count;
        }

        public void NextLine()
        {
            currentLineIndex += texts.Count;
            Invalidate();
        }

        protected override void Update()
        {
            SplitText();
            UpdateTextComponents();
        }

        private void UpdateTextComponents()
        {
            for (int i = 0; i < texts.Count; i++)
                texts[i].Text = splitter.GetString(i + currentLineIndex);
        }

        private void SplitText()
        {
            splitter.SplitText(texts[0].DisplayableChars(), text);
        }

        protected virtual ITextGraphicComponent CreateTextComponent(ISpriteFont font)
        {
            return new TextBox(font, Game);
        }
    }
}