using GameEngine.Graphics.Layouts;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Graphics.Basic
{
    public class Frame : IGraphicComponent
    {
        private IGraphicComponent box;
        public void AddContent(IGraphicComponent component)
        {
            Layout.AddComponent(component);
        }
        public Frame(String backgroundTexture)
        {
            box = new TextureBox(backgroundTexture);
            box.PositionChanged += box_PositionChanged;
            box.SizeChanged += box_SizeChanged;
            Layout = new SingleComponentLayout();

            SetMargins(90, 90, 80, 80);
        }

        public event EventHandler<EventArgs> PositionChanged;

        public event EventHandler<EventArgs> SizeChanged;

        public float Height { get { return box.Height; } set { box.Height = value; } }
        public ILayout Layout { get; set; }
        public float Width { get { return box.Width; } set { box.Width = value; } }
        public float X { get { return box.X; } set { box.X = value; } }
        public float Y { get { return box.Y; } set { box.Y = value; } }

        public void Draw(GameTime time, ISpriteBatch batch)
        {
            box.Draw(time, batch);
            Layout.Draw(time, batch);
        }

        public void SetMargins(int left = 0, int right = 0, int top = 0, int bottom = 0)
        {
            if (Layout == null)
                return;

            Layout.SetMargin(left, right, top, bottom);
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

        private void box_PositionChanged(object sender, EventArgs e)
        {
            if (PositionChanged != null)
                PositionChanged(this, null);
        }

        private void box_SizeChanged(object sender, EventArgs e)
        {
            if (SizeChanged != null)
                SizeChanged(this, null);
        }
    }
}