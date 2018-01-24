using System.Collections.Generic;
using System.Linq;
using GameEngine.Globals;
using GameEngine.GUI.Components;

namespace GameEngine.GUI.Controlls
{
    public class TextArea : AbstractGuiComponent
    {
        public int NumberOfLines { get; }
        private string _text;
        internal readonly ITextSplitter _splitter;

        public IEnumerable<TextAreaLine> Lines => _allLines.Skip(CurrentLineIndex).Take(NumberOfLines);

        internal List<TextAreaLine> _allLines;
        public int CurrentLineIndex { get; set; }

        public int TextHeight { get; set; } = 32;

        public TextArea(ITextSplitter splitter = null, int numberOfLines = 2)
        {
            NumberOfLines = numberOfLines;
            _splitter = splitter ?? new DefaultTextSplitter();
        }

        public string Text
        {
            set
            {
                _text = value;
                CurrentLineIndex = 0;
                Invalidate();
            }
            get => _text;
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

            CurrentLineIndex++;
            Invalidate();
        }
        public override void HandleKeyInput(CommandKeys key)
        {
            if (key == CommandKeys.Select)
                NextLine();
        }
    }
}