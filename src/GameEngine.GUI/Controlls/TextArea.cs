using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Globals;
using GameEngine.Graphics.General;
using GameEngine.GUI.Components;
using GameEngine.GUI.Renderers;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Controlls
{
    [GameType]
    public class TextArea : AbstractGuiComponent
    {
        public int NumberOfLines { get; }
        private string _text;
        private readonly ITextSplitter _splitter;
        private readonly TextAreaRenderer _renderer;

        public IEnumerable<TextAreaLine> Lines => _allLines.Skip(CurrentLineIndex).Take(NumberOfLines);

        private List<TextAreaLine> _allLines;
        public int CurrentLineIndex { get; set; }

        public int TextHeight { get; set; } = 32;

        public TextArea(TextAreaRenderer renderer, ITextSplitter splitter, int numberOfLines = 2)
        {
            NumberOfLines = numberOfLines;
            _splitter = splitter;
            _renderer = renderer;
        }

        public string Text
        {
            set
            {
                _text = value;
                CurrentLineIndex = 0;
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

            return _allLines != null && CurrentLineIndex + NumberOfLines < _allLines.Count;
        }

        public void NextLine()
        {
            if (!HasNext())
                return;

            MoveLines();
        }

        private void MoveLines()
        {
            var nextIndex = Math.Min(_allLines.Count - 1, CurrentLineIndex + NumberOfLines);
            var linesToMove = nextIndex - CurrentLineIndex;

            var lineHeight = _renderer.GetLineHeight(this);
            var moveVector = linesToMove * new Vector2(0, lineHeight);

            foreach (var line in Lines)
            {
                line.Position += moveVector;
            }
            CurrentLineIndex = nextIndex;
        }

        protected override void Update()
        {
            _allLines = _splitter
                .SplitText(_renderer.CharsPerLine(this), Text)
                .Select(x => new TextAreaLine{Text = x})
                .ToList();

            var lineHeight = _renderer.GetLineHeight(this);
            for(var i = 0; i < _allLines.Count; i++)
            {
                _allLines[i].Position = new Vector2(Area.X, Area.Y + + lineHeight);
                i++;
            }
            CurrentLineIndex = Math.Min(CurrentLineIndex, _allLines.Count - 1);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _renderer.Render(batch, this);
        }

        public override void HandleKeyInput(CommandKeys key)
        {
            if(key == CommandKeys.Select)
                NextLine();
        }
    }
}