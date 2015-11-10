using BattleLib.GraphicComponents.MenuView;
using GameEngine;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BattleLib.GraphicComponents
{
    public class BattleGraphics : AbstractGraphicComponentOld
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

            line.Start = new Vector2(0.3f * Engine.ScreenWidth, 0.3f * Engine.ScreenHeight);
            line.End = new Vector2(0.6f * Engine.ScreenWidth, 0.3f * Engine.ScreenHeight);
            line.Scale = 0.05f * screenHeight;
            line.Color = Color.DarkViolet;

            line2.X = 0.0f;
            line2.Y = 0.3f * Engine.ScreenHeight;
            line2.Width = 1.0f * Engine.ScreenWidth;
            line2.Heigth = 0.05f * Engine.ScreenHeight;
            line2.Color = Color.Black;

            MessageBox.Setup(content);

            MessageBox.X = 0;
            MessageBox.Y = 2.0f * Engine.ScreenHeight / 3.0f;
            MessageBox.Width = Engine.ScreenWidth;
            MessageBox.Height = Engine.ScreenHeight - MessageBox.Y;

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
            MessageBox.Draw(time, batch);
            Menu.Draw(time, batch, screenWidth, screenHeight);
            line.Draw(batch);
            line2.Draw(batch, screenWidth, screenHeight);
        }
    }
}
