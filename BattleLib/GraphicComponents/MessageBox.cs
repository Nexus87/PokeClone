using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BattleLib.GraphicComponents
{
    public class MessageBox : AbstractGraphicComponent
    {
        Rectangle Constraints = new Rectangle();
        readonly Vector2 margin = new Vector2(50, 30);
        Texture2D border;
        SpriteFont font;
        public String Text{ private get; set; }

        public MessageBox()
        {
            Text = "";
        }
        public override void Draw(Vector2 Origin, SpriteBatch batch, GameTime gameTime)
        {
            Constraints.Location = Origin.ToPoint();
            Vector2 textVec = Origin + margin;
            batch.Draw(border,Constraints, Color.White);
            batch.DrawString(font, Text, textVec, Color.Black);
        }

        public override void Setup(Rectangle screen, ContentManager content)
        {
            border = content.Load<Texture2D>("border");
            font = content.Load<SpriteFont>("MenuFont");
        }


        public override void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeight)
        {
            Constraints.X = 0;
            Constraints.Y = (int)(2.0f * screenHeight / 3.0f);

            Constraints.Width = screenWidth;
            Constraints.Height = screenHeight - Constraints.Y;

            batch.Draw(border, Constraints, Color.White);
            batch.DrawString(font, Text, Constraints.Location.ToVector2() + margin, Color.Black);
        }

    }
}
