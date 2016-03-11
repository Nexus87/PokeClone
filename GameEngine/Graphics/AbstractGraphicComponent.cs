using GameEngine.Graphics.Basic;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Graphics
{
    public abstract class AbstractGraphicComponent : IGraphicComponent
    {
        public PokeEngine Game { get; protected set; }
        protected AbstractGraphicComponent(PokeEngine game)
        {
            Game = game;
        }

        private bool needsUpdate = true;
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
                if (size.Y == value)
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
                if (size.X == value)
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
                if (position.X == value)
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
                if (position.Y == value)
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

            if (needsUpdate)
            {
                Update();
                needsUpdate = false;
            }
            DrawComponent(time, batch);
        }


        protected abstract void DrawComponent(GameTime time, ISpriteBatch batch);

        protected void Invalidate()
        {
            needsUpdate = true;
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

        public abstract void Setup(ContentManager content);
    }
}