using GameEngine;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BattleLib.GraphicComponents
{
    public class MessageBox : IGraphicComponent
    {
        float marginX = 0.06f * Engine.ScreenWidth;
        float marginY = 0.07f * Engine.ScreenHeight;
        float x;
        float y;
        TextureBox box = new TextureBox("border");
        GraphicText text = new GraphicText("MenuFont");

        public String Text{ private get; set; }

        public MessageBox()
        {
            X = 0;
            Y = 0;
            box.Y = 2.0f * Engine.ScreenHeight/ 3.0f;
            box.Width = 1 * Engine.ScreenWidth;
            box.Height = 1.0f * Engine.ScreenHeight / 3.0f;

            text.X = (box.X + 0.05f) * Engine.ScreenWidth;
            text.Y = (box.Y + 0.05f) * Engine.ScreenHeight;

            text.Text = "A";
        }


        public float X
        {
            get { return x; }
            set { x = value; box.X = x; text.X = x + marginX; }
        }

        public float Y 
        { 
            get { return y; } 
            set { y = value; box.Y = y; text.Y = y + marginY; } 
        }

        public float Width { get { return box.Width; } set { box.Width = value; } }
        public float Height { get { return box.Height; } set { box.Height = value; } }

        public void Draw(GameTime time, SpriteBatch batch)
        {
            box.Draw(time, batch);
            text.Draw(batch);
        }

        public void Setup(ContentManager content)
        {
            box.Setup(content);
            text.Setup(content);
        }
    }
}
