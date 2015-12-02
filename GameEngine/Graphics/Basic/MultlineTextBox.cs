using GameEngine.Graphics.Layouts;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace GameEngine.Graphics.Basic
{
    public class MultlineTextBox : AbstractGraphicComponent
    {
        private readonly LinkedList<string> lines = new LinkedList<string>();
        private readonly List<TextBox> texts = new List<TextBox>();
        private VBoxLayout layout = new VBoxLayout();
        private int lineNumber;
        private string text;

        public MultlineTextBox(int lineNumber, string fontName, ISpriteFont font)
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
            return lines.Count == 0;
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
            string remaining = text;
            int limit = CharsPerLine();
            if (limit == 0)
                return;

            while (remaining.Length > limit)
            {
                int length = remaining.Substring(0, limit).LastIndexOf(" ");

                if (length == -1)
                    length = limit;

                lines.AddLast(remaining.Substring(0, length));
                remaining = remaining.Substring(length + 1);
            }

            lines.AddLast(remaining);
            lines.AddLast("");
        }
    }
}