using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameEngine.Graphics.Widgets
{
    public class MessageBox : AbstractGraphicComponent, IWidget
    {
        public Keys SelectKey;

        private TextureBox frameBox;

        private SingleComponentLayout layout = new SingleComponentLayout();

        private MultlineTextBox textBox;

        public MessageBox(Configuration config)
            : this(config.BoxBorder, config)
        {
        }

        public MessageBox(string border, Configuration config)
        {
            SelectKey = config.KeySelect;
            frameBox = new TextureBox(border);
            textBox = new MultlineTextBox(config.MenuFont);
        }

        public event EventHandler OnAllLineShowed = delegate { };

        public void DisplayText(string text)
        {
            textBox.Text = text;
        }

        public void HandleInput(Keys key)
        {
            if (key != SelectKey)
                return;

            if (textBox.HasNext())
                textBox.NextLine();
            else
                OnAllLineShowed(this, null);
        }

        public void ResetText()
        {
            textBox.Text = "";
        }

        public override void Setup(ContentManager content)
        {
            var boxLayout = new SingleComponentLayout();
            layout.Init(this);
            layout.AddComponent(frameBox);

            boxLayout.Init(frameBox);
            boxLayout.AddComponent(textBox);
            boxLayout.SetMargin(10, 10, 10, 10);

            layout.Setup(content);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            layout.Draw(time, batch);
        }
    }
}