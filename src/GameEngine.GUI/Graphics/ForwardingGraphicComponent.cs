using System;
using System.Collections.Generic;
using GameEngine.Globals;
using GameEngine.GUI.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics
{
    /// <summary>
    /// Skeletal implementation of an IGraphicComponent to utilize composition.
    /// </summary>
    /// <remarks>
    /// The goal of this class is, to make it easier to use composition, by forwarding
    /// calls to an inner IGraphicComponent.
    /// This is especially useful for subclasses, that want to use layouts, with a fixed
    /// number of components. To do this, a subclass must only use T = Container.
    /// </remarks>
    /// <typeparam name="T">Inner IGraphicComponent</typeparam>
    /// 
    public abstract class ForwardingGraphicComponent<T> : IGraphicComponent where T : IGraphicComponent
    {
        /// <summary>
        /// Inner IGraphicComponent
        /// </summary>
        /// <remarks>
        /// This property provides access to the inner component. It can only be
        /// set in the constructor.
        /// </remarks>
        protected T InnerComponent { get; set; }
        private bool _needsUpdate = true;

        public event EventHandler<GraphicComponentSizeChangedEventArgs> PreferredSizeChanged
        {
            add { InnerComponent.PreferredSizeChanged += value; }
            remove { InnerComponent.PreferredSizeChanged -= value; }
        }

        /// <summary>
        /// Signals that either Width or Height changed.
        /// </summary>
        /// <see cref="IGraphicComponent.SizeChanged"/>
        public event EventHandler<GraphicComponentSizeChangedEventArgs> SizeChanged
        {
            add { InnerComponent.SizeChanged += value; }
            remove { InnerComponent.SizeChanged -= value; }
        }

        /// <summary>
        /// Constructs an instance of this class with InnerComponent component
        /// </summary>
        /// <param name="component">Inner component</param>
        protected ForwardingGraphicComponent(T component)
        {
            InnerComponent = component;
        }
        /// <summary>
        /// This function is called when the component needs to be drawn
        /// </summary>
        /// <param name="time">Game time</param>
        /// <param name="batch">Sprite batch</param>
        /// <see cref="IGraphicComponent.Draw"/>
        public virtual void Draw(GameTime time, ISpriteBatch batch)
        {
            if (_needsUpdate)
            {
                Update();
                _needsUpdate = false;
            }
            InnerComponent.Draw(time, batch);
        }

        /// <summary>
        /// This function loads the content, needed for this component
        /// </summary>
        /// <see cref="IGraphicComponent.Setup"/>
        public virtual void Setup()
        {
            InnerComponent.Setup();
        }

        /// <summary>
        /// By calling this function, a subclass can signal that its components need to be updated 
        /// </summary>
        /// <remarks>
        /// Calling this function has the effect, that at the beginning of the next Draw call,
        /// the Update method is executed.
        /// </remarks>
        protected void Invalidate()
        {
            _needsUpdate = true;
        }

        /// <summary>
        /// In this function, a subclass can Update itself
        /// </summary>
        /// <remarks>
        /// This function is only called, if Invalidate was called before.
        /// It is called at the beginning of Draw, before any drawing is done.
        /// </remarks>
        protected abstract void Update();

        /// <summary>
        /// Plays the given animation
        /// </summary>
        /// <param name="animation">Animation</param>
        /// <see cref="IGraphicComponent.PlayAnimation"/>
        public void PlayAnimation(IAnimation animation)
        {
            InnerComponent.PlayAnimation(animation);
        }


        public Color Color { get; set; }

        public float PreferredHeight => InnerComponent.PreferredHeight;

        public float PreferredWidth => InnerComponent.PreferredWidth;

        public ResizePolicy HorizontalPolicy
        {
            get
            {
                return InnerComponent.HorizontalPolicy;
            }

            set
            {
                InnerComponent.HorizontalPolicy = value;
            }
        }

        public ResizePolicy VerticalPolicy
        {
            get
            {
                return InnerComponent.VerticalPolicy;
            }

            set
            {
                InnerComponent.VerticalPolicy = value;
            }
        }

        public Rectangle ScissorArea {
            get { return InnerComponent.ScissorArea; }
            set { InnerComponent.ScissorArea = value;}
        }

        public Rectangle Area
        {
            get { return InnerComponent.Area; }
            set { InnerComponent.Area = value; }
        }

        public IGraphicComponent Parent
        {
            get { return InnerComponent.Parent; }
            set { InnerComponent.Parent = value; }
        }

        public IEnumerable<IGraphicComponent> Children => InnerComponent.Children;
        public bool IsSelected { get; set; }
        public bool IsSelectable { get; set; }
        public virtual void HandleKeyInput(CommandKeys key)
        {
            InnerComponent.HandleKeyInput(key);
        }

        public event EventHandler<VisibilityChangedEventArgs> VisibilityChanged
        {
            add { InnerComponent.VisibilityChanged += value; }
            remove { InnerComponent.VisibilityChanged -= value; }
        }

        public bool IsVisible
        {
            get
            {
                return InnerComponent.IsVisible;
            }
            set
            {
                InnerComponent.IsVisible = value;
            }
        }
    }
}