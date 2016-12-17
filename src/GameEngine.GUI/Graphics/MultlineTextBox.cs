using System.Collections.Generic;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Panels;
using GameEngine.Registry;
using GameEngine.Utils;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics
{
    [GameType]
    public class MultlineTextBox : AbstractGraphicComponent, ITextGraphicContainer
    {
        private string _text;
        private readonly ITextSplitter _splitter;
        private int _currentLineIndex;
        private readonly int _lineNumber;
        private readonly ISpriteFont _font;
        private readonly List<ITextGraphicComponent> _textBoxes = new List<ITextGraphicComponent>();
        private readonly Grid _grid = new Grid();

        public MultlineTextBox(ISpriteFont font, ITextSplitter splitter, int lineNumber)
        {
            _lineNumber = lineNumber;
            _font = font;
            _splitter = splitter;
        }

        public string Text
        {
            set
            {
                _text = value;
                _currentLineIndex = 0;
                Invalidate();
            }
            get
            {
                return _text;
            }
        }

        public bool HasNext()
        {
            if (NeedsUpdate)
            {
                Update();
                NeedsUpdate = false;
            }

            return _currentLineIndex + _lineNumber < _splitter.Count;
        }

        public void NextLine()
        {
            if (!HasNext())
                return;

            _currentLineIndex += _lineNumber;
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
            _grid.SetCoordinates(this);
        }

        private void UpdateTextComponents()
        {
            for (int i = 0; i < _lineNumber; i++)
            {
                var line = _splitter.GetString(i + _currentLineIndex);
                _textBoxes[i].Text = line;
            }
        }

        private void SplitText()
        {
            _splitter.SplitText(_textBoxes[0].DisplayableChars(), _text);
        }

        protected virtual ITextGraphicComponent CreateTextComponent(ISpriteFont font)
        {
            return new TextBox(font);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _grid.Draw(time, batch);
        }

        public override void Setup()
        {
            _grid.AddPercentColumn();
            for (var i = 0; i < _lineNumber; i++)
            {
                var component = CreateTextComponent(_font);
                component.Setup();
                _textBoxes.Add(component);
                _grid.AddRow(new RowProperty{Type = ValueType.Percent, Share = 1});
                _grid.SetComponent(component, i, 0);
            }
        }
    }
}