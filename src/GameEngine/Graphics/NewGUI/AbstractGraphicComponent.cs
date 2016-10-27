using System;
using System.Collections.Generic;
using GameEngine.Graphics.NewGUI.Renderers;
using Microsoft.Xna.Framework;

namespace GameEngine.Graphics.NewGUI
{
    public abstract class AbstractGraphicComponent : IGraphicComponent
    {
        public static readonly NullRenderer NullRenderer = new NullRenderer();

        protected readonly List<IGraphicComponent> ChildrenList;
        private float _preferedWidth;
        private float _preferedHeight;

        protected AbstractGraphicComponent()
        {
            ChildrenList = new List<IGraphicComponent>();
        }

        public event EventHandler PreferedSizeChanged;
        public Rectangle Constraints { get; set; }
        public Rectangle ScissorArea { get; set; }

        public float PreferedWidth
        {
            get { return _preferedWidth; }
            protected set
            {
                if(Math.Abs(_preferedWidth - value) > 1e-15)
                    return;
                _preferedWidth = value;
                OnPreferedSizeChanged();
            }
        }

        public float PreferedHeight
        {
            get { return _preferedHeight; }
            protected set
            {
                if(Math.Abs(_preferedHeight - value) > 1e-15)
                    return;
                _preferedHeight = value;
                OnPreferedSizeChanged();
            }
        }


        public bool IsSelectable { get; protected set; }
        public IGraphicComponent Parent { get; protected set; }

        public IEnumerable<IGraphicComponent> Children => ChildrenList;

        public IRenderer Renderer { get; protected set; } = NullRenderer;
        public virtual bool IsSelected { get; set; }

        public virtual void Update(GameTime time)
        {
        }

        public abstract void HandleKeyInput(CommandKeys key);

        protected virtual void OnPreferedSizeChanged()
        {
            PreferedSizeChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}