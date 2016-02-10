using System;

namespace GameEngine.Graphics
{
    public abstract class ForwardingGraphicComponent<T> : IGraphicComponent where T : IGraphicComponent
    {
        protected T InnerComponent { get; private set; }
        private bool needsUpdate;

        public event EventHandler<GraphicComponentPositionChangedArgs> PositionChanged = delegate { };
        public event EventHandler<GraphicComponentSizeChangedArgs> SizeChanged = delegate { };

        public ForwardingGraphicComponent(T component, PokeEngine game)
        {
            Game = game;
            InnerComponent = component;
            InnerComponent.PositionChanged += (a, b) => { PositionChanged(this, b); };
            InnerComponent.SizeChanged += (a, b) => { SizeChanged(this, b); };
        }

        public PokeEngine Game { get; protected set; }

        public float Height { get { return InnerComponent.Height; } set { InnerComponent.Height = value; } }
        public float Width { get { return InnerComponent.Width; } set { InnerComponent.Width = value; } }
        public float X { get { return InnerComponent.X; } set { InnerComponent.X = value; } }
        public float Y { get { return InnerComponent.Y; } set { InnerComponent.Y = value; } }

        public void Draw(Microsoft.Xna.Framework.GameTime time, Wrapper.ISpriteBatch batch)
        {
            if (needsUpdate)
            {
                Update();
                needsUpdate = false;
            }
            InnerComponent.Draw(time, batch);
        }

        public void Setup(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            InnerComponent.Setup(content);
        }

        protected void Invalidate()
        {
            needsUpdate = true;
        }

        protected abstract void Update();
    }
}