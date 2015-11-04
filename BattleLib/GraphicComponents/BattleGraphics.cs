using BattleLib.GraphicComponents.MenuView;
using BattleLib.GraphicComponents.Util;
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
        Line line;
        Line line2;
        Texture2D pkmn;

        public override void Setup(Rectangle screen, ContentManager content)
        {
            line = new Line();
            line2 = new Line();

            screenWidth = screen.Size.X;
            screenHeight = screen.Size.Y;

            line.Start = new Vector2(0.3f * screenWidth, 0.3f * screenHeight);
            line.End = new Vector2(0.6f * screenWidth, 0.3f * screenHeight);
            line.Scale = 0.05f * screenHeight;
            line.Color = Color.DarkViolet;

            line2.X = 0.0f;
            line2.Y = 0.3f;
            line2.Width = 1.0f;
            line2.Heigth = 0.05f;
            line2.Color = Color.Black;

            var border = content.Load<Texture2D>("border");
            var arrow = content.Load<Texture2D>("arrow");
            var font = content.Load<SpriteFont>("MenuFont");

            MessageBox.Setup(screen, content);
            Menu.Setup(content);
            line.Setup(content);
            line2.Setup(content);
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
            line.Draw(batch);
            line2.Draw(batch, screenWidth, screenHeight);
        }
    }
}
