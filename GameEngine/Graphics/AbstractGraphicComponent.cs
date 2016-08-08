using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Graphics
{
    public abstract class AbstractGraphicComponent : IGraphicComponent
    {
        protected AbstractGraphicComponent()
        {
            NeedsUpdate = true;
        }

        protected bool NeedsUpdate { get; set; }
        private Vector2 position;
        private Vector2 size;

        public event EventHandler<GraphicComponentPositionChangedEventArgs> PositionChanged = (a, b) => { };
        public event EventHandler<GraphicComponentSizeChangedEventArgs> SizeChanged = (a, b) => { };

        public float Height
        {
            get { return size.Y; }
            set
            {
                if (value.CompareTo(0) < 0)
                    throw new ArgumentException("Height must be >=0");
                if (size.Y.AlmostEqual(value))
                    return;
                size.Y = value;
                Invalidate();
                SizeChanged(this, new GraphicComponentSizeChangedEventArgs(size.X, size.Y));
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
                if (value.CompareTo(0) < 0)
                    throw new ArgumentException("Width must be >=0");
                if (size.X.AlmostEqual(value))
                    return;
                size.X = value;
                Invalidate();
                SizeChanged(this, new GraphicComponentSizeChangedEventArgs(size.X, size.Y));
            }
        }

        public float XPosition
        {
            get { return position.X; }
            set
            {
                if (position.X.AlmostEqual(value))
                    return;

                position.X = value;
                Invalidate();
                PositionChanged(this, new GraphicComponentPositionChangedEventArgs(position.X, position.Y));
            }
        }

        public float YPosition
        {
            get { return position.Y; }
            set
            {
                if (position.Y.AlmostEqual(value))
                    return;

                position.Y = value;
                Invalidate();
                PositionChanged(this, new GraphicComponentPositionChangedEventArgs(position.X, position.Y));
            }
        }

        protected Vector2 Position { get { return position; } }
        protected Vector2 Size { get { return size; } }
        
        public void Draw(GameTime time, ISpriteBatch batch)
        {
            if (Animation != null)
                Animation.Update(time, this);

            if (NeedsUpdate)
            {
                Update();
                NeedsUpdate = false;
            }
            DrawComponent(time, batch);
        }


        protected abstract void DrawComponent(GameTime time, ISpriteBatch batch);

        protected void Invalidate()
        {
            NeedsUpdate = true;
        }

        protected virtual void Update()
        {
        }

        public void PlayAnimation(IAnimation animation)
        {
            Animation = animation;
            Animation.AnimationFinished += Animation_AnimationFinished;
        }

        private void Animation_AnimationFinished(object sender, EventArgs e)
        {
            Animation.AnimationFinished -= Animation_AnimationFinished;
            Animation = null;
        }

        protected IAnimation Animation { get; set; }

        public abstract void Setup();


        public Color Color { get; set; }

        public virtual float PreferedHeight { get; set; }
        public virtual float PreferedWidth { get; set; }
        public ResizePolicy HorizontalPolicy { get; set; }
        public ResizePolicy VerticalPolicy { get; set; }
    }
}