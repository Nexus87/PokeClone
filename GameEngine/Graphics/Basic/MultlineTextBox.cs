using GameEngine.Graphics.Layouts;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GameEngine.Graphics.Basic
{
    public class MultlineTextBox : ForwardingGraphicComponent<Container>
    {
        private readonly LinkedList<string> lines = new LinkedList<string>();
        private readonly List<TextBox> texts = new List<TextBox>();
        private string text;

        public MultlineTextBox(string fontName, PokeEngine game) : this(fontName, game, 2) { }
        public MultlineTextBox(string fontName, PokeEngine game, int lineNumber)
            : this(fontName, lineNumber, new XNASpriteFont(fontName, game.Content), game)
        { }

        public MultlineTextBox(string fontName, int lineNumber, ISpriteFont font, PokeEngine game)
            : base(new Container(game), game)
        {
            var container = InnerComponent;
            container.Layout = new VBoxLayout();

            for (int i = 0; i < lineNumber; i++)
            {
                var box = new TextBox(fontName, font, game);
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
        public string Text { set { text = value; Invalidate(); } }
        public bool HasNext()
        {
            return lines.Count != 0;
        }
        public void NextLine()
        {
            SetText();
        }

        protected override void Update()
        {
            SplitText();
            SetText();
        }

        private void SetText()
        {
            int i = 0;

            while (lines.Count > 0 && i < texts.Count)
            {
                texts[i].Text = lines.First.Value;
                lines.RemoveFirst();
                i++;
            }

            for (; i < texts.Count; i++)
                texts[i].Text = "";
        }

        private void SplitText()
        {
            if (text == null)
                return;
            string remaining = text.Trim();
            int limit = CharsPerLine();
            if (limit == 0)
                return;

            while (remaining.Length > limit)
            {
                var match = Regex.Match(remaining.Substring(0, limit), @"\s", RegexOptions.RightToLeft);
                int length = match.Success ? match.Index : limit;


                lines.AddLast(remaining.Substring(0, length).Trim());
                remaining = remaining.Substring(length).TrimStart();
            }

            // remaining has no trailing whitespace, because we trimmed it at the beginning.
            if(remaining.Length != 0)
                lines.AddLast(remaining);
        }
    }
}