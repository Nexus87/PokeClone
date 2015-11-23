using GameEngine.Graphics.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameEngine.Graphics.Widgets
{
    public class MessageBox : AbstractGraphicComponent, IWidget
    {
        public Keys SelectKey = Keys.Enter;
        private string Text;
        private TextBox textBox;
        private SingleComponentLayout layout = new SingleComponentLayout();

        public MessageBox(string border, Configuration config)
        {
            SelectKey = config.KeySelect;
        }

        public MessageBox(string border)
        {
        }

        public void HandleInput(Keys key)
        {

        }

        public override void Setup(ContentManager content)
        {
            throw new NotImplementedException();
        }

        protected override void DrawComponent(GameTime time, SpriteBatch batch)
        {
            throw new NotImplementedException();
        }
    }
}