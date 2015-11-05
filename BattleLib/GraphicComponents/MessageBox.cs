using BattleLib.GraphicComponents.Util;
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
        TextureBox box = new TextureBox("border");
        TextGraphic text = new TextGraphic("MenuFont");

        public String Text{ private get; set; }

        public MessageBox()
        {
            Text = "abc";
            box.Y = 2.0f / 3.0f;
            box.Width = 1;
            box.Height = 1.0f / 3.0f;

            text.X = box.X + 0.05f;
            text.Y = box.Y + 0.05f;

            text.Text = "abc";
        }

        public override void Setup(Rectangle screen, ContentManager content)
        {
            border = content.Load<Texture2D>("border");
            font = content.Load<SpriteFont>("MenuFont");
            
            box.Setup(content);
            text.Setup(content);
        }


        public override void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeight)
        {
            Constraints.X = 0;
            Constraints.Y = (int)(2.0f * screenHeight / 3.0f);

            Constraints.Width = screenWidth;
            Constraints.Height = screenHeight - Constraints.Y;

            //box.Draw(batch);
            text.Draw(batch);
            //batch.DrawString(font, Text, Constraints.Location.ToVector2() + margin, Color.Black);
        }

    }
}
