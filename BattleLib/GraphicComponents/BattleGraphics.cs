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

        private MessageBox messageBox;
        public MenuGraphics Menu { get; set; }
        Line line1;
        Line line2;
        Texture2D pkmn;

        public override void Setup(Rectangle screen, ContentManager content)
        {
            line2 = new Line();
            line1 = new Line();
            messageBox = new MessageBox();

            screenWidth = screen.Size.X;
            screenHeight = screen.Size.Y;


            line2.X = 50.0f;
            line2.Y = 0.4f * Engine.ScreenHeight;
            line2.Width = 0.8f * Engine.ScreenWidth;
            line2.Height = 50.0f;
            line2.Color = Color.Black;

            line1.X = line2.X + 10.0f;
            line1.Y = line2.Y + 10.0f;
            line1.Width = 0.5f * (line2.Width - 40.0f);
            line1.Height = line2.Height - 20.0f;
            line1.Color = Color.Green;

            messageBox.X = 0;
            messageBox.Y = 2.0f * Engine.ScreenHeight / 3.0f;
            messageBox.Width = Engine.ScreenWidth;
            messageBox.Height = Engine.ScreenHeight - messageBox.Y;
            messageBox.Setup(content);



            Menu.Setup(content);
            line1.Setup(content);
            line2.Setup(content);
        }


        public void DisplayText(String text)
        {
            if(messageBox != null)
                messageBox.Text = text;
        }

        public void ClearText()
        {
            DisplayText("");
        }

        public override void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeight)
        {
            if (time.ElapsedGameTime.Seconds == 0)
            {
                line1.Width += 5.0f;
                if (line1.Width > (line2.Width - 40.0f))
                    line1.Width = 0.0f;
            }
            messageBox.Draw(time, batch);
            //Menu.Draw(time, batch, screenWidth, screenHeight);
            line2.Draw(time, batch);
            line1.Draw(time, batch);
        }
    }
}
