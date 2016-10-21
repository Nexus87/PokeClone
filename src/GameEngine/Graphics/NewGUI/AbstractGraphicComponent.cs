using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameEngine.Graphics.NewGUI
{
    public abstract class AbstractGraphicComponent : IGraphicComponent
    {
        protected readonly List<IGraphicComponent> ChildrenList;
        protected Vector2 Position;
        protected Vector2 Size;
        private float preferedWidth;
        private float preferedHeight;

        protected AbstractGraphicComponent()
        {
            ChildrenList = new List<IGraphicComponent>();
        }

        public event EventHandler PreferedSizeChanged;
        public Rectangle Constraints { get; set; }
        public Rectangle ScissorArea { get; set; }

        public float PreferedWidth
        {
            get { return preferedWidth; }
            protected set
            {
                if(Math.Abs(preferedWidth - value) > 1e-15)
                    return;
                preferedWidth = value;
                OnPreferedSizeChanged();
            }
        }

        public float PreferedHeight
        {
            get { return preferedHeight; }
            protected set
            {
                if(Math.Abs(preferedHeight - value) > 1e-15)
                    return;
                preferedHeight = value;
                OnPreferedSizeChanged();
            }
        }

        public ResizeBehavoir VerticalBehavoir { get; set; }
        public ResizeBehavoir HorizontalBehavoir { get; set; }


        public IGraphicComponent Parent { get; protected set; }

        public IEnumerable<IGraphicComponent> Children
        {
            get { return ChildrenList; }
        }

        public IRenderer Renderer { get; protected set; }
        public abstract void Update(GameTime time);

        protected virtual void OnPreferedSizeChanged()
        {
            var handler = PreferedSizeChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}