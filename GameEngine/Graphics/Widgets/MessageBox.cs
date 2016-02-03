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

        public MessageBox(Configuration config, PokeEngine game)
            : this(config.BoxBorder, config, game)
        {
        }

        public MessageBox(string border, Configuration config, PokeEngine game)
            : base(game)
        {
            SelectKey = config.KeySelect;
            frameBox = new TextureBox(border, game);
            textBox = new MultlineTextBox(config.MenuFont, game);
        }

        public event EventHandler OnAllLineShowed = delegate { };

        public event EventHandler<VisibilityChangedArgs> OnVisibilityChanged;

        public bool IsVisible { get; private set; }

        public void DisplayText(string text)
        {
            textBox.Text = text;
        }

        public bool HandleInput(Keys key)
        {
            if (key != SelectKey)
                return false;

            if (textBox.HasNext())
                textBox.NextLine();
            else
                OnAllLineShowed(this, null);

            return true;
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