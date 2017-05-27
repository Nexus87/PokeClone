using System;
using System.Collections.Generic;
using GameEngine.Globals;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI
{
    public abstract class AbstractGuiComponent : IGuiComponent
    {
        public event EventHandler<ComponentSelectedEventArgs> ComponentSelected;

        protected AbstractGuiComponent()
        {
            NeedsUpdate = true;
        }

        protected bool NeedsUpdate { get; set; }
        private float _preferredHeight;
        private float _preferredWidth;
        protected readonly List<IGuiComponent> children = new List<IGuiComponent>();
        private Rectangle _area;
        private bool _isSelected;

        public event EventHandler<GraphicComponentSizeChangedEventArgs> PreferredSizeChanged;


        protected void OnPreferredSizeChanged(GraphicComponentSizeChangedEventArgs eventArgs)
        {
            PreferredSizeChanged?.Invoke(this, eventArgs);
        }


        protected void Invalidate()
        {
            NeedsUpdate = true;
        }


        public virtual float PreferredHeight {
            get
            {
                return _preferredHeight;
            }
            set
            {
                if (value.AlmostEqual(_preferredHeight))
                    return;
                _preferredHeight = value;
                OnPreferredSizeChanged(new GraphicComponentSizeChangedEventArgs(this, PreferredWidth, PreferredHeight));
            }
        }

        public virtual float PreferredWidth
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
                OnPreferredSizeChanged(new GraphicComponentSizeChangedEventArgs(this, PreferredWidth, PreferredHeight));
            }
        }

        public Rectangle ScissorArea { get; set; }

        public virtual Rectangle Area
        {
            get { return _area; }
            set
            {
                if(_area == value)
                    return;

                _area = value;
                Invalidate();
            }
        }

        public IGuiComponent Parent { get; set; }

        public IEnumerable<IGuiComponent> Children => children;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if(_isSelected == value)
                    return;
                _isSelected = value;
                Invalidate();
                if(_isSelected)
                    OnComponentSelected(new ComponentSelectedEventArgs{Source = this});
            }
        }

        public bool IsSelectable { get; protected set; }

        public virtual void Update()
        {
        }

        public virtual void HandleKeyInput(CommandKeys key)
        {
        }

        protected virtual void OnComponentSelected(ComponentSelectedEventArgs e)
        {
            ComponentSelected?.Invoke(this, e);
        }
    }
}