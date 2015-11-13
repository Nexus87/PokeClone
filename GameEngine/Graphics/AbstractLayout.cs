using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Graphics
{
    public abstract class AbstractLayout : ILayout
    {
        protected IGraphicComponent parent;
        private float marginBottom;
        private float marginLeft;
        private float marginRight;
        private float marginTop;
        private bool needsUpdate = true;
        private Vector2 position;
        private Vector2 size;
        protected float Height { get { return size.Y - marginTop - marginBottom; } }
        protected float Width { get { return size.X - marginRight - marginLeft; } }
        protected float X { get { return position.X + marginRight; } }
        protected float Y { get { return position.Y + marginTop; } }

        public abstract void AddComponent(IGraphicComponent component);

        public void Draw(GameTime time, SpriteBatch batch)
        {
            if (needsUpdate)
            {
                UpdateComponents();
                needsUpdate = false;
            }

            DrawComponents(time, batch);
        }

        public void Init(IGraphicComponent component)
        {
            parent = component;
            parent.SizeChanged += parent_ConstraintsChanged;
            parent.PositionChanged += parent_ConstraintsChanged;
            UpdateCoordinates();
        }

        public abstract void RemoveComponent(IGraphicComponent component);

        public void SetMargin(int left = 0, int right = 0, int top = 0, int bottom = 0)
        {
            marginLeft = left;
            marginRight = right;
            marginTop = top;
            marginBottom = bottom;

            Invalidate();
        }

        public abstract void Setup(ContentManager content);

        protected abstract void DrawComponents(GameTime time, SpriteBatch batch);

        protected void Invalidate()
        {
            needsUpdate = true;
        }

        protected abstract void UpdateComponents();

        private void parent_ConstraintsChanged(object sender, EventArgs e)
        {
            UpdateCoordinates();
        }

        private void UpdateCoordinates()
        {
            if (parent == null)
                return;

            position.X = parent.X;
            position.Y = parent.Y;
            size.X = parent.Width;
            size.Y = parent.Height;

            Invalidate();
        }
    }
}