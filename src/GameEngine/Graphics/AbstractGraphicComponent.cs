using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Graphics
{
    public abstract class AbstractGraphicComponent : IGraphicComponent
    {
        private bool isVisible = true;

        public event EventHandler<VisibilityChangedEventArgs> VisibilityChanged = delegate { };

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (value == isVisible)
                    return;

                isVisible = value;
                VisibilityChanged(this, new VisibilityChangedEventArgs(isVisible));
            }
        }

        protected AbstractGraphicComponent()
        {
            NeedsUpdate = true;
        }

        protected bool NeedsUpdate { get; set; }
        private Vector2 position;
        private Vector2 size;
        private float preferredHeight;
        private float preferredWidth;

        public event EventHandler<GraphicComponentPositionChangedEventArgs> PositionChanged = (a, b) => { };
        public event EventHandler<GraphicComponentSizeChangedEventArgs> SizeChanged = (a, b) => { };
        public event EventHandler<GraphicComponentSizeChangedEventArgs> PreferredSizeChanged = (a, b) => { };

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
                SizeChanged(this, new GraphicComponentSizeChangedEventArgs(this, size.X, size.Y));
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
                SizeChanged(this, new GraphicComponentSizeChangedEventArgs(this, size.X, size.Y));
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
            if (!isVisible)
                return;

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

        public float PreferredHeight {
            get
            {
                return preferredHeight;
            }
            protected set
            {
                if (value.AlmostEqual(preferredHeight))
                    return;
                preferredHeight = value;
                PreferredSizeChanged(this, new GraphicComponentSizeChangedEventArgs(this, PreferredWidth, PreferredHeight));
            }
        }
        public float PreferredWidth
        {
            get
            {
                return preferredWidth;
            }
            set
            {
                if (value.AlmostEqual(preferredWidth))
                    return;
                preferredWidth = value;
                PreferredSizeChanged(this, new GraphicComponentSizeChangedEventArgs(this, PreferredWidth, PreferredHeight));
            }
        }
        public ResizePolicy HorizontalPolicy { get; set; }
        public ResizePolicy VerticalPolicy { get; set; }
    }
}