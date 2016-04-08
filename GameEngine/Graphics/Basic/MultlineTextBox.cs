using GameEngine.Graphics.Layouts;
using GameEngine.Wrapper;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System;

namespace GameEngine.Graphics.Basic
{
    public class MultlineTextBox : AbstractGraphicComponent
    {
        private string text;
        private ITextSplitter splitter;
        private int currentLineIndex = 0;
        private Container container;
        private int lineNumber;
        private ISpriteFont font;

        public MultlineTextBox(PokeEngine game) : this(2, game) 
        { }
        
        public MultlineTextBox(int lineNumber, PokeEngine game)
            : this(game.DefaultFont,  new DefaultTextSplitter(), lineNumber, game)
        { }

        private ITextGraphicComponent GetComponent(int index)
        {
            return (ITextGraphicComponent) container.Components[index];
        }

        public MultlineTextBox(ISpriteFont font, ITextSplitter splitter, int lineNumber, PokeEngine game) :
            base(game)
        {
            this.lineNumber = lineNumber;
            this.font = font;
            this.splitter = splitter;
            container = new Container(game);
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
            currentLineIndex += lineNumber;
        }

        protected override void Update()
        {
            container.SetCoordinates(this);
            container.ForceLayout();
            SplitText();
            UpdateTextComponents();
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
            return new TextBox(font, Game);
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