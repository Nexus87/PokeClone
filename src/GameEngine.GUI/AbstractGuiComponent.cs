using System;
using System.Collections.Generic;
using GameEngine.Globals;
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

        public bool NeedsUpdate { get; set; }
        private float _preferredHeight;
        private float _preferredWidth;
        private Rectangle _area;
        private bool _isSelected;

        public event EventHandler<GraphicComponentSizeChangedEventArgs> PreferredSizeChanged;


        private void OnPreferredSizeChanged(GraphicComponentSizeChangedEventArgs eventArgs)
        {
            PreferredSizeChanged?.Invoke(this, eventArgs);
        }


        protected void Invalidate()
        {
            NeedsUpdate = true;
        }


        public virtual float PreferredHeight {
            get => _preferredHeight;
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
            get => _preferredWidth;
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
            get => _area;
            set
            {
                if(_area == value)
                    return;

                _area = value;
                Invalidate();
            }
        }

        public IGuiComponent Parent { get; set; }

        public List<IGuiComponent> Children { get; } = new List<IGuiComponent>();

        public bool IsSelected
        {
            get => _isSelected;
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

        protected void OnComponentSelected(ComponentSelectedEventArgs e)
        {
            ComponentSelected?.Invoke(this, e);
        }
    }
}