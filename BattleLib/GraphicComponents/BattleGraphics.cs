using BattleLib.GraphicComponents.MenuView;
using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BattleLib.GraphicComponents
{
    public class BattleGraphics : AbstractGraphicComponent
    {
        int screenWidth;
        int screenHeight;

        public MessageBox MessageBox { get; set; }
        public MenuGraphics Menu { get; set; }

        public override void Setup(Rectangle screen, ContentManager content)
        {
            screenWidth = screen.Size.X;
            screenHeight = screen.Size.Y;

            var border = content.Load<Texture2D>("border");
            var arrow = content.Load<Texture2D>("arrow");
            var font = content.Load<SpriteFont>("MenuFont");

            MessageBox.Setup(screen, content);
            Menu.Setup(content);
        }


        public void DisplayText(String text)
        {
            if(MessageBox != null)
                MessageBox.Text = text;
        }

        public void ClearText()
        {
            DisplayText("");
        }

        public override void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeight)
        {
            MessageBox.Draw(time, batch, screenWidth, screenHeight);
            Menu.Draw(time, batch, screenWidth, screenHeight);
        }
    }
}
