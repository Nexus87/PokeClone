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
    public abstract class BoxLayout : ILayout
    {
        private Vector2 position;
        private Vector2 size;

        private float marginLeft;
        private float marginRight;
        private float marginTop;
        private float marginBottom;

        private bool needsUpdate = true;

        protected readonly LinkedList<IGraphicComponent> components = new LinkedList<IGraphicComponent>();
        protected float X { get { return position.X + marginRight; } }
        protected float Y { get { return position.Y + marginTop; } }

        protected float Width { get { return size.X - marginRight - marginLeft; } }
        protected float Height { get { return size.Y - marginTop - marginBottom; } }

        protected void Invalidate()
        {
            needsUpdate = true;
        }

        public void Init(IGraphicComponent component)
        {
            position.X = component.X;
            position.Y = component.Y;

            size.X = component.Width;
            size.Y = component.Height;
        }

        public void AddComponent(IGraphicComponent component)
        {
            components.AddLast(component);
        }

        public void SetMargin(int left = 0, int right = 0, int top = 0, int bottom = 0)
        {
            marginLeft = left;
            marginRight = right;
            marginTop = top;
            marginBottom = bottom;
        }

        public void Setup(ContentManager content)
        {
            foreach (var component in components)
                component.Setup(content);
        }

        public void Draw(GameTime time, SpriteBatch batch)
        {
            if (needsUpdate)
                Update();

            foreach (var component in components)
                component.Draw(time, batch);
        }

        private void Update()
        {
            InitComponents();
            needsUpdate = false;
        }

        protected abstract void InitComponents();
    }
}
