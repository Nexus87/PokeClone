using GameEngine.Utils;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace GameEngine.Graphics.Basic
{
    public class MultiLayeredComponent : AbstractGraphicComponent
    {
        readonly private NullGraphicObject nullObject;

        private IGraphicComponent background;
        private IGraphicComponent foreground;
        private IGraphicComponent mainComponent;

        public MultiLayeredComponent(IGraphicComponent mainComponent, PokeEngine game) : base(game)
        {
            nullObject = new NullGraphicObject(game);
            this.mainComponent = mainComponent;
            foreground = nullObject;
            background = nullObject;
        }

        public IGraphicComponent Background
        {
            get { return background; }
            set
            {
                if (value == null)
                {
                    background = nullObject;
                    return;
                }
                background = value;
                Invalidate();
            }
        }

        public IGraphicComponent Foreground
        {
            get { return foreground; }
            set
            {
                if (value == null)
                {
                    foreground = nullObject;
                    return;
                }

                foreground = value;
                Invalidate();
            }
        }

        public IGraphicComponent MainComponent
        {
            get { return mainComponent; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("MainComponent must not be null");

                mainComponent = value;
                Invalidate();
            }
        }

        public override void Setup(ContentManager content)
        {
            foreground.Setup(content);
            background.Setup(content);
            mainComponent.Setup(content);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            background.Draw(time, batch);
            mainComponent.Draw(time, batch);
            foreground.Draw(time, batch);
        }

        protected override void Update()
        {
            background.SetCoordinates(this);
            foreground.SetCoordinates(this);
            mainComponent.SetCoordinates(this);
        }
    }
}