using System;
using System.Collections.Generic;
using GameEngine.GUI.Renderers;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI
{
    public abstract class AbstractGraphicComponent : IGraphicComponent
    {
        public static readonly NullRenderer NullRenderer = new NullRenderer();

        protected readonly List<IGraphicComponent> ChildrenList = new List<IGraphicComponent>();
        private float _preferedWidth;
        private float _preferedHeight;

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
        public IGraphicComponent Parent { get; set; }

        public IEnumerable<IGraphicComponent> Children => ChildrenList;

        public virtual IRenderer Renderer { get; protected set; } = NullRenderer;
        public virtual bool IsSelected { get; set; }

        public virtual void Update(GameTime time)
        {
        }

        public abstract void HandleKeyInput(CommandKeys key);
        public virtual void Init()
        {

        }

        protected virtual void OnPreferedSizeChanged()
        {
            PreferedSizeChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}