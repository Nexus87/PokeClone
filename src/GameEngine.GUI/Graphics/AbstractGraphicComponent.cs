using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using GameEngine.Graphics.General;

namespace GameEngine.Graphics
{
    public abstract class AbstractGraphicComponent : IGraphicComponent
    {
        private bool _isVisible = true;

        public event EventHandler<VisibilityChangedEventArgs> VisibilityChanged = delegate { };

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (value == _isVisible)
                    return;

                _isVisible = value;
                VisibilityChanged(this, new VisibilityChangedEventArgs(_isVisible));
            }
        }

        protected AbstractGraphicComponent()
        {
            NeedsUpdate = true;
        }

        protected bool NeedsUpdate { get; set; }
        private Vector2 _position;
        private Vector2 _size;
        private float _preferredHeight;
        private float _preferredWidth;
        protected readonly List<IGraphicComponent> children = new List<IGraphicComponent>();

        public event EventHandler<GraphicComponentSizeChangedEventArgs> SizeChanged = (a, b) => { };
        public event EventHandler<GraphicComponentSizeChangedEventArgs> PreferredSizeChanged = (a, b) => { };

        public float Height
        {
            get { return _size.Y; }
            set
            {
                if (value.CompareTo(0) < 0)
                    throw new ArgumentException("Height must be >=0");
                if (_size.Y.AlmostEqual(value))
                    return;
                _size.Y = value;
                Invalidate();
                SizeChanged(this, new GraphicComponentSizeChangedEventArgs(this, _size.X, _size.Y));
            }
        }

        public float Width
        {
            get
            {
                return _size.X;
            }
            set
            {
                if (value.CompareTo(0) < 0)
                    throw new ArgumentException("Width must be >=0");
                if (_size.X.AlmostEqual(value))
                    return;
                _size.X = value;
                Invalidate();
                SizeChanged(this, new GraphicComponentSizeChangedEventArgs(this, _size.X, _size.Y));
            }
        }

        public float XPosition
        {
            get { return _position.X; }
            set
            {
                if (_position.X.AlmostEqual(value))
                    return;

                _position.X = value;
                Invalidate();
            }
        }

        public float YPosition
        {
            get { return _position.Y; }
            set
            {
                if (_position.Y.AlmostEqual(value))
                    return;

                _position.Y = value;
                Invalidate();
            }
        }

        protected Vector2 Position { get { return _position; } }
        protected Vector2 Size { get { return _size; } }

        public void Draw(GameTime time, ISpriteBatch batch)
        {
            if (Animation != null)
                Animation.Update(time, this);
            if (!_isVisible)
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
                return _preferredHeight;
            }
            protected set
            {
                if (value.AlmostEqual(_preferredHeight))
                    return;
                _preferredHeight = value;
                PreferredSizeChanged(this, new GraphicComponentSizeChangedEventArgs(this, PreferredWidth, PreferredHeight));
            }
        }
        public float PreferredWidth
        {
            get
            {
                return _preferredWidth;
            }
            set
            {
                if (value.AlmostEqual(_preferredWidth))
                    return;
                _preferredWidth = value;
                PreferredSizeChanged(this, new GraphicComponentSizeChangedEventArgs(this, PreferredWidth, PreferredHeight));
            }
        }
        public ResizePolicy HorizontalPolicy { get; set; }
        public ResizePolicy VerticalPolicy { get; set; }
        public Rectangle ScissorArea { get; set; }
        public Rectangle Area => new Rectangle(_position.ToPoint(), _size.ToPoint());
        public IGraphicComponent Parent { get; set; }

        public IEnumerable<IGraphicComponent> Children => children;
    }
}