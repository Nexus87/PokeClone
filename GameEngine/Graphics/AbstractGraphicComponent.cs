using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Graphics
{
    public abstract class AbstractGraphicComponent : IGraphicComponent
    {
        private bool needsUpdate = true;
        private Vector2 position;
        private Vector2 size;

        public event EventHandler<EventArgs> PositionChanged = (a, b) => { };
        public event EventHandler<EventArgs> SizeChanged = (a, b) => { };

        public float Height
        {
            get { return size.Y; }
            set
            {
                if (size.Y == value)
                    return;
                size.Y = value;
                Invalidate();
                SizeChanged(this, null);
            }
        }

        public float Width
        {
            get
            {
                return size.X;
            }
            set
            {
                if (size.X == value)
                    return;
                size.X = value;
                Invalidate();
                SizeChanged(this, null);
            }
        }

        public float X
        {
            get { return position.X; }
            set
            {
                if (position.X == value)
                    return;

                position.X = value;
                Invalidate();
                PositionChanged(this, null);
            }
        }

        public float Y
        {
            get { return position.Y; }
            set
            {
                if (position.Y == value)
                    return;

                position.Y = value;
                Invalidate();
                PositionChanged(this, null);
            }
        }

        protected Vector2 Position { get { return position; } }
        protected Vector2 Size { get { return size; } }

        public void Draw(GameTime time, ISpriteBatch batch)
        {
            if (needsUpdate)
            {
                Update();
                needsUpdate = false;
            }
            DrawComponent(time, batch);
        }

        public abstract void Setup(ContentManager content);

        protected abstract void DrawComponent(GameTime time, ISpriteBatch batch);

        protected void Invalidate()
        {
            needsUpdate = true;
        }

        protected virtual void Update()
        {
        }
    }
}