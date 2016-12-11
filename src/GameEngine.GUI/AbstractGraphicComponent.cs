using System;
using System.Collections.Generic;
using GameEngine.Globals;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI
{
    public abstract class AbstractGraphicComponent : IGuiComponent
    {
        protected readonly List<IGraphicComponent> ChildrenList = new List<IGraphicComponent>();
        private float _preferedWidth;
        private float _preferedHeight;
        private bool _needsUpdate = true;
        private Rectangle _constraints;

        public event EventHandler PreferedSizeChanged;

        public Rectangle Constraints
        {
            get { return _constraints; }
            set
            {
                if (_constraints == value)
                    return;
                Invalidate();

                _constraints = value;
            }
        }

        public Rectangle ScissorArea { get; set; }

        public float PreferedWidth
        {
            get { return _preferedWidth; }
            protected set
            {
                if (Math.Abs(_preferedWidth - value) > 1e-15)
                    return;
                _preferedWidth = value;
                OnPreferedSizeChanged();
            }
        }

        protected void Invalidate()
        {
            _needsUpdate = true;
        }

        public float PreferedHeight
        {
            get { return _preferedHeight; }
            protected set
            {
                if (Math.Abs(_preferedHeight - value) > 1e-15)
                    return;
                _preferedHeight = value;
                OnPreferedSizeChanged();
            }
        }


        public bool IsSelectable { get; set; }
        public IGraphicComponent Parent { get; set; }

        public IEnumerable<IGraphicComponent> Children => ChildrenList;

        public virtual bool IsSelected { get; set; }

        protected virtual void Update(GameTime time) { }

        public abstract void HandleKeyInput(CommandKeys key);

        public void Draw(GameTime time, ISpriteBatch spriteBatch)
        {
            if (_needsUpdate)
                Update(time);
            DrawComponent(time, spriteBatch);
        }

        protected virtual void DrawComponent(GameTime time, ISpriteBatch spriteBatch) { }

        public virtual void Init() { }

        protected virtual void OnPreferedSizeChanged()
        {
            PreferedSizeChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}