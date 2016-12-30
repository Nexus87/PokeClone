using System;
using System.Collections.Generic;
using GameEngine.GUI.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics
{
    /// <summary>
    /// Interface for all drawable 2D components
    /// </summary>
    /// <remarks>
    /// This interface is the equivalent of the GameComponent for drawable 2D objects,
    /// but instead of the Update method it has Draw, which also takes a SpriteBatch
    /// 
    /// A IGraphicComponent is a rectangle, with measures Width/Height and position
    /// X/Y. 
    /// Every subtype is only allowed to draw inside this rectangle
    /// 
    /// Changed of the position or size is signaled by the corresponding event
    /// SizeChanged and PositionChanged.
    /// </remarks>
    public interface IGraphicComponent : IInputHandler
    {
        /// <summary>
        /// Event is triggered, when IsVisible changes
        /// </summary>
        event EventHandler<VisibilityChangedEventArgs> VisibilityChanged;

        event EventHandler<ComponentSelectedEventArgs> ComponentSelected;
        /// <summary>
        /// This property shows, if the component is visible.
        /// </summary>
        bool IsVisible { get; set; }

        /// <summary>
        /// Play the given animation
        /// </summary>
        /// <param name="animation">Animation to play</param>
        void PlayAnimation(IAnimation animation);

        /// <summary>
        /// This event signals that either Width or Height has changed
        /// </summary>
        event EventHandler<GraphicComponentSizeChangedEventArgs> SizeChanged;

        event EventHandler<GraphicComponentSizeChangedEventArgs> PreferredSizeChanged;

        /// <summary>
        /// Called when the component needs to be drawn
        /// </summary>
        /// <param name="time">Time passed since the last call to Draw</param>
        /// <param name="batch">Current SpriteBatch</param>
        void Draw(GameTime time, ISpriteBatch batch);

        /// <summary>
        /// In this function, the component can load its resources
        /// </summary>
        /// <remarks>
        /// This function needs to be called before the first call to Draw.
        /// If the component is registered to the PokeEngine, the function will
        /// be after Run().
        /// </remarks>
        void Setup();

        Color Color { get; set; }

        float PreferredHeight { get; }
        float PreferredWidth { get; }
       
        ResizePolicy HorizontalPolicy { get; set; }
        ResizePolicy VerticalPolicy { get; set; }

        Rectangle ScissorArea { get; set; }
        Rectangle Area { get; set; }

        IGraphicComponent Parent { get; set; }
        IEnumerable<IGraphicComponent> Children { get; }
        bool IsSelected { get; set; }
        bool IsSelectable { get; }
    }

    public class ComponentSelectedEventArgs : EventArgs
    {
        public IGraphicComponent Source { get; set; }
    }
}
