using System;
using System.Collections.Generic;
using GameEngine.Globals;
using GameEngine.GUI.General;
using GameEngine.GUI.Graphics;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI
{
    public abstract class AbstractGraphicComponent : IGraphicComponent
    {
        private bool _isVisible = true;

        public event EventHandler<VisibilityChangedEventArgs> VisibilityChanged = delegate { };
        public event EventHandler<ComponentSelectedEventArgs> ComponentSelected;

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (value == _isVisible)
                    return;

                _isVisible = value;
                VisibilityChanged(this, new VisibilityChangedEventArgs(_isVisible));
            }
        }

        protected AbstractGraphicComponent()
        {
            NeedsUpdate = true;
        }

        protected bool NeedsUpdate { get; set; }
        private float _preferredHeight;
        private float _preferredWidth;
        protected readonly List<IGraphicComponent> children = new List<IGraphicComponent>();
        private Rectangle _area;
        private bool _isSelected;

        public event EventHandler<GraphicComponentSizeChangedEventArgs> PreferredSizeChanged;


        protected void OnPreferredSizeChanged(GraphicComponentSizeChangedEventArgs eventArgs)
        {
            PreferredSizeChanged?.Invoke(this, eventArgs);
        }
        public void Draw(GameTime time, ISpriteBatch batch)
        {
            Animation?.Update(time, this);
            if (!_isVisible)
                return;

            if (NeedsUpdate)
            {
                Update();
                NeedsUpdate = false;
            }
            DrawComponent(time, batch);
        }


        protected virtual void DrawComponent(GameTime time, ISpriteBatch batch)
        {
        }

        protected void Invalidate()
        {
            NeedsUpdate = true;
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

        public virtual void Setup(){}

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

        public IGraphicComponent Parent { get; set; }

        public IEnumerable<IGraphicComponent> Children => children;

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

        public virtual void HandleKeyInput(CommandKeys key)
        {
        }

        protected virtual void OnComponentSelected(ComponentSelectedEventArgs e)
        {
            ComponentSelected?.Invoke(this, e);
        }
    }
}