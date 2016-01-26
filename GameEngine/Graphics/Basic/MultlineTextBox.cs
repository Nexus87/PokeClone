using GameEngine.Graphics.Layouts;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GameEngine.Graphics.Basic
{
    public class MultlineTextBox : AbstractGraphicComponent
    {
        private readonly LinkedList<string> lines = new LinkedList<string>();
        private readonly List<TextBox> texts = new List<TextBox>();
        private VBoxLayout layout = new VBoxLayout();
        private int lineNumber;
        private string text;

        public MultlineTextBox(string fontName, int lineNumber = 2)
            : this(fontName, lineNumber, new XNASpriteFont())
        { }

        public MultlineTextBox(string fontName, int lineNumber, ISpriteFont font)
        {
            this.lineNumber = lineNumber;
            for (int i = 0; i < lineNumber; i++)
            {
                var box = new TextBox(fontName, font);
                texts.Add(box);
                layout.AddComponent(box);
            }
        }

        public int CharsPerLine()
        {
            // If we don't force the layout to update, DisplayableChars might
            // return 0
            layout.ForceUpdateComponents();
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

        public override void Setup(ContentManager content)
        {
            layout.Init(this);
            layout.Setup(content);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            layout.Draw(time, batch);
        }

        protected override void Update()
        {
            base.Update();
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
                var sub = remaining.Substring(0, limit);
                var match = Regex.Match(remaining.Substring(0, limit), @"\s", RegexOptions.RightToLeft);
                int length = match.Success ? match.Index : limit;


                lines.AddLast(remaining.Substring(0, length).Trim());
                remaining = remaining.Substring(length).TrimStart();
            }

            // remaining has no trailing whitespace, because we trimmed it at the beginning.
            if(remaining != "")
                lines.AddLast(remaining);
        }
    }
}