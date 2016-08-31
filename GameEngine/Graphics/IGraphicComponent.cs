using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Graphics
{
    /// <summary>
    /// This determines how layouts resize components in a container
    /// </summary>
    public enum ResizePolicy
    {
        /// <summary>
        /// This is the default value. Layouts will resize the components width/height as needed
        /// </summary>
        Extending,
        /// <summary>
        /// The layout will try to resize the component according to its preferred width/height but
        /// also takes care, that the component is inside the constraints of the container.
        /// </summary>
        Preferred,
        /// <summary>
        /// The layout will not resize the component even if its bigger than the surrounding
        /// container.
        /// </summary>
        Fixed
    }

    /// <summary>
    /// Argument for IWidgets OnVisibleChanged event.
    /// </summary>
    public class VisibilityChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The new visibility of the widget.
        /// </summary>
        public bool Visible { get; private set; }
        /// <summary>
        /// Constructs an instance.
        /// </summary>
        /// <param name="visible">New visibility of the widget</param>
        public VisibilityChangedEventArgs(bool visible)
        {
            Visible = visible;
        }
    }


    public class GraphicComponentSizeChangedEventArgs : EventArgs
    {
        public IGraphicComponent Component { get; private set; }
        public float Width { get; private set; }
        public float Height { get; private set; }

        public GraphicComponentSizeChangedEventArgs(IGraphicComponent component, float width, float height)
        {
            Component = component;
            Width = width;
            Height = height;
        }
    }

    public class GraphicComponentPositionChangedEventArgs : EventArgs
    {
        public float XPosition { get; private set; }
        public float YPosition { get; private set; }

        public GraphicComponentPositionChangedEventArgs(float xPosition, float yPosition)
        {
            XPosition = xPosition;
            YPosition = yPosition;
        }
    }

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
    public interface IGraphicComponent
    {
        /// <summary>
        /// Event is triggered, when IsVisible changes
        /// </summary>
        event EventHandler<VisibilityChangedEventArgs> VisibilityChanged;

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
        /// <summary>
        /// This event signals that either X or Y has changed
        /// </summary>
        event EventHandler<GraphicComponentPositionChangedEventArgs> PositionChanged;

        event EventHandler<GraphicComponentSizeChangedEventArgs> PreferredSizeChanged;

        /// <summary>
        /// Current X position
        /// </summary>
        float XPosition { get; set; }
        /// <summary>
        /// Current Y position
        /// </summary>
        float YPosition { get; set; }

        /// <summary>
        /// Current Width
        /// </summary>
        float Width { get; set; }
        /// <summary>
        /// Current Height
        /// </summary>
        float Height { get; set; }

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

    }
}
