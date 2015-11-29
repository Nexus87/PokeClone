using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Graphics.Layouts;
using GameEngine.Wrapper;

namespace GameEngine.Graphics.Basic
{
    public class MultlineTextBox : AbstractGraphicComponent
    {
        public bool ClearOnNext { get; set; }
        public string Text { set { text = value; Invalidate(); } }
        string text;
        int lineNumber;
        readonly List<TextBox> texts = new List<TextBox>();
        readonly LinkedList<string> lines = new LinkedList<string>();
        VBoxLayout layout = new VBoxLayout();

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
            int limit = texts[0].DisplayableChars();
            while(remaining.Length > limit)
            {
                int length = remaining.Substring(0, limit).LastIndexOf(" ");
                if (length == -1)
                    length = limit;
                lines.AddLast(remaining.Substring(0, length));
                remaining = remaining.Substring(length);
                    
            }

            lines.AddLast(remaining);
        }

        public void NextLine()
        {
            // If there is only one line, it needs to be cleared
            // in order to show a new one
            if (ClearOnNext || lineNumber == 1)
            {
                SetText();
                return;
            }

            // We have at least 2 lines here
            for(int i = 0; i < texts.Count - 1; i++)
            {
                texts[i].Text = texts[i + 1].Text;
            }

            texts[texts.Count - 1].Text = lines.Count > 0 ? lines.First.Value : "";
            if(lines.Count != 0)
                lines.RemoveFirst();
        }
    }
}
