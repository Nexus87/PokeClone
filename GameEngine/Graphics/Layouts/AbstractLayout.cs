using GameEngine.Graphics.Basic;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace GameEngine.Graphics.Layouts
{
    public abstract class AbstractLayout : ILayout
    {
        private float marginBottom;
        private float marginLeft;
        private float marginRight;
        private float marginTop;
        private bool needsUpdate = true;

        private Vector2 position;
        private Vector2 size;
        protected float Height { get { return Math.Max(0, size.Y - marginTop - marginBottom); } }
        protected float Width { get { return Math.Max(0, size.X - marginRight - marginLeft); } }
        protected float X { get { return position.X + marginLeft; } }
        protected float Y { get { return position.Y + marginTop; } }

        public void LayoutContainer(Container container)
        {
            position.X = container.X;
            position.Y = container.Y;
            size.X = container.Width;
            size.Y = container.Height;

            UpdateComponents(container);
        }



        public void SetMargin(int left = 0, int right = 0, int top = 0, int bottom = 0)
        {
            marginLeft = left;
            marginRight = right;
            marginTop = top;
            marginBottom = bottom;
        }

        protected abstract void UpdateComponents(Container container);

    }
}