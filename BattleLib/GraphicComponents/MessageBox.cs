using GameEngine;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BattleLib.GraphicComponents
{
    public class MessageBox : AbstractGraphicComponentOld
    {
        Rectangle Constraints = new Rectangle();
        readonly Vector2 margin = new Vector2(50, 30);
        Texture2D border;
        SpriteFont font;
        TextureBox box = new TextureBox("border");
        Frame frame;
        TextBox text = new TextBox("MenuFont");

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
            frame = new Frame("border");
            var layout = new TableLayout(2, 2);
            layout.AddComponent(0, 0, new TextureBox("border"));
            layout.AddComponent(0, 1, new TextureBox("border"));
            layout.AddComponent(1, 0, new TextureBox("border"));
            layout.AddComponent(1, 1, new TextureBox("border"));
            frame.Layout = layout;
            frame.X = 0.0f;
            frame.Y = 2.0f/3.0f;
            frame.Width = 1.0f;
            frame.Height = 1.0f / 3.0f;
        }

        public override void Setup(Rectangle screen, ContentManager content)
        {
            border = content.Load<Texture2D>("border");
            font = content.Load<SpriteFont>("MenuFont");
            
            box.Setup(content);
            text.Setup(content);
            frame.Setup(content);
        }


        public override void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeight)
        {
            //box.Draw(batch);
            //text.Draw(time, batch);
            //batch.DrawString(font, Text, Constraints.Location.ToVector2() + margin, Color.Black);
            frame.Draw(time, batch);
        }

    }
}
