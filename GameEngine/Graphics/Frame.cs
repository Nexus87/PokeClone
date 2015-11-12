using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public class Frame : IGraphicComponent
    {
        public event EventHandler<EventArgs> SizeChanged;
        public event EventHandler<EventArgs> PositionChanged;

        public float X { get { return box.X; } set { box.X = value; } }
        public float Y { get { return box.Y; } set { box.Y = value; } }
        public float Width { get { return box.Width; } set { box.Width = value; } }
        public float Height { get { return box.Height; } set { box.Height = value; } }

        public ILayout Layout { get; set; }
        IGraphicComponent box;
  
        public Frame(String backgroundTexture)
        {
            box = new TextureBox(backgroundTexture);
            box.PositionChanged += box_PositionChanged;
            box.SizeChanged += box_SizeChanged;
            Layout = new SingleComponentLayout();
        }

        void box_SizeChanged(object sender, EventArgs e)
        {
            if (SizeChanged != null)
                SizeChanged(this, null);
        }

        void box_PositionChanged(object sender, EventArgs e)
        {
            if (PositionChanged != null)
                PositionChanged(this, null);
        }

        public void Draw(GameTime time, SpriteBatch batch)
        {
            box.Draw(time, batch);
            Layout.Draw(time, batch);
        }

        public void Setup(ContentManager content)
        {
            if (box == null)
                return;

            box.Setup(content);

            if (Layout == null)
                return;

            Layout.Init(box);
            Layout.Setup(content);
        }

        public void SetMargins(int left = 0, int right = 0, int top = 0, int bottom = 0)
        {
            if (Layout == null)
                return;

            Layout.SetMargin(left, right, top, bottom);
        }

    }
}
