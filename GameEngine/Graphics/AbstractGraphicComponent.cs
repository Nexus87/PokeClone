using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Graphics
{
    public abstract class AbstractGraphicComponent : IGraphicComponent
    {
        public AbstractGraphicComponent(Game game) : base(game) { }
        private bool needsUpdate = true;
        private Vector2 position;
        private Vector2 size;

        public override event EventHandler<GraphicComponentPositionChangedArgs> PositionChanged = (a, b) => { };
        public override event EventHandler<GraphicComponentSizeChangedArgs> SizeChanged = (a, b) => { };

        public override float Height
        {
            get { return size.Y; }
            set
            {
                if (value.CompareTo(0) < 0)
                    throw new ArgumentException("Height must be >=0");
                if (size.Y == value)
                    return;
                size.Y = value;
                Invalidate();
                SizeChanged(this, new GraphicComponentSizeChangedArgs(size.X, size.Y));
            }
        }

        public override float Width
        {
            get
            {
                return size.X;
            }
            set
            {
                if (value.CompareTo(0) < 0)
                    throw new ArgumentException("Width must be >=0");
                if (size.X == value)
                    return;
                size.X = value;
                Invalidate();
                SizeChanged(this, new GraphicComponentSizeChangedArgs(size.X, size.Y));
            }
        }

        public override float X
        {
            get { return position.X; }
            set
            {
                if (position.X == value)
                    return;

                position.X = value;
                Invalidate();
                PositionChanged(this, new GraphicComponentPositionChangedArgs(position.X, position.Y));
            }
        }

        public override float Y
        {
            get { return position.Y; }
            set
            {
                if (position.Y == value)
                    return;

                position.Y = value;
                Invalidate();
                PositionChanged(this, new GraphicComponentPositionChangedArgs(position.X, position.Y));
            }
        }

        protected Vector2 Position { get { return position; } }
        protected Vector2 Size { get { return size; } }

        public override void Draw(GameTime time, ISpriteBatch batch)
        {
            if (needsUpdate)
            {
                Update();
                needsUpdate = false;
            }
            DrawComponent(time, batch);
        }

        //public abstract void Setup(ContentManager content);

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