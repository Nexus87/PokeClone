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
    public abstract class AbstractLayout : ILayout
    {
        protected IGraphicComponent parent;
        private Vector2 position;
        private Vector2 size;

        private float marginLeft;
        private float marginRight;
        private float marginTop;
        private float marginBottom;

        protected float X { get { return position.X + marginRight; } }
        protected float Y { get { return position.Y + marginTop; } }

        protected float Width { get { return size.X - marginRight - marginLeft; } }
        protected float Height { get { return size.Y - marginTop - marginBottom; } }

        private bool needsUpdate = true;

        protected void Invalidate()
        {
            needsUpdate = true;
        }

        public void Init(IGraphicComponent component)
        {
            parent = component;
            parent.SizeChanged += parent_ConstraintsChanged;
            parent.PositionChanged += parent_ConstraintsChanged;
            UpdateCoordinates();
        }

        void parent_ConstraintsChanged(object sender, EventArgs e)
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

        public void SetMargin(int left = 0, int right = 0, int top = 0, int bottom = 0)
        {
            marginLeft = left;
            marginRight = right;
            marginTop = top;
            marginBottom = bottom;

            Invalidate();
        }

        public void Draw(GameTime time, SpriteBatch batch)
        {
            if(needsUpdate){
                UpdateComponents();
                needsUpdate = false;
            }

            DrawComponents(time, batch);
        }

        public abstract void Setup(ContentManager content);
        public abstract void AddComponent(IGraphicComponent component);
        protected abstract void DrawComponents(GameTime time, SpriteBatch batch);
        protected abstract void UpdateComponents();
    }
}
