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
        //TextGraphic text = new TextGraphic("MenuFont");
        TextBox textBox = new TextBox("MenuFont");
        public String Text{ private get; set; }

        public MessageBox()
        {
            X = 0;
            Y = 0;
            box.Y = 2.0f * Engine.ScreenHeight/ 3.0f;
            box.Width = 1 * Engine.ScreenWidth;
            box.Height = 1.0f * Engine.ScreenHeight / 3.0f;

            textBox.X = (box.X + 0.05f) * Engine.ScreenWidth;
            textBox.Y = (box.Y + 0.05f) * Engine.ScreenHeight; ;
            textBox.Width = box.Width - 0.1f * Engine.ScreenWidth;
            textBox.Height = box.Height - 0.05f *Engine.ScreenHeight;
            textBox.Text = "012345678901234567890123456789012345678901234567890123456789012345678901234567890";
        }


        public float X
        {
            get { return x; }
            set { x = value; box.X = x; textBox.X = x; }
        }

        public float Y 
        { 
            get { return y; }
            set { y = value; box.Y = y; textBox.Y = y; } 
        }

        public float Width { get { return box.Width; } set { box.Width = value; } }
        public float Height { get { return box.Height; } set { box.Height = value; } }

        public void Draw(GameTime time, SpriteBatch batch)
        {
            box.Draw(time, batch);
            textBox.Draw(time, batch);
            //text.Draw(batch);
        }

        public void Setup(ContentManager content)
        {
            box.Setup(content);
            textBox.Setup(content);
            //text.Setup(content);
        }
    }
}
