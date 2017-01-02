using System;
using System.Collections.Generic;
using GameEngine.Globals;
using GameEngine.Graphics.General;
using GameEngine.GUI.Graphics;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI
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
        event EventHandler<ComponentSelectedEventArgs> ComponentSelected;

        /// <summary>
        /// Play the given animation
        /// </summary>
        /// <param name="animation">Animation to play</param>
        void PlayAnimation(IAnimation animation);

        event EventHandler<GraphicComponentSizeChangedEventArgs> PreferredSizeChanged;

        /// <summary>
        /// Called when the component needs to be drawn
        /// </summary>
        /// <param name="time">Time passed since the last call to Draw</param>
        /// <param name="batch">Current SpriteBatch</param>
        void Draw(GameTime time, ISpriteBatch batch);

        float PreferredHeight { get; }
        float PreferredWidth { get; }

        Rectangle ScissorArea { get; set; }
        Rectangle Area { get; set; }

        IGraphicComponent Parent { get; set; }
        IEnumerable<IGraphicComponent> Children { get; }

        bool IsSelected { get; set; }
        bool IsSelectable { get; }
    }
}
