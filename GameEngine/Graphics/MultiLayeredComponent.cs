using GameEngine.Utils;
using Microsoft.Xna.Framework;

namespace GameEngine.Graphics
{
    public class MultiLayeredComponent : AbstractGraphicComponent
    {
        readonly private NullGraphicObject nullObject;

        private IGraphicComponent background;
        private IGraphicComponent foreground;
        private IGraphicComponent mainComponent;

        public MultiLayeredComponent(IGraphicComponent mainComponent)
        {
            nullObject = new NullGraphicObject();
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
                value.CheckNull("value");

                mainComponent = value;
                Invalidate();
            }
        }

        public override void Setup()
        {
            foreground.Setup();
            background.Setup();
            mainComponent.Setup();
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