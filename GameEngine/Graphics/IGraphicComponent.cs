using GameEngine.Graphics.Basic;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Graphics
{
    public class GraphicComponentSizeChangedEventArgs : EventArgs
    {
        public float Width { get; private set; }
        public float Height { get; private set; }

        public GraphicComponentSizeChangedEventArgs(float width, float height)
        {
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
        /// Instance of the current running PokeEngine
        /// </summary>
        PokeEngine Game { get; }

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
        /// <param name="content"></param>
        void Setup(ContentManager content);
    }
}
