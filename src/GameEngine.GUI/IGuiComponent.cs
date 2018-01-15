using System;
using System.Collections.Generic;
using GameEngine.Globals;
using GameEngine.Graphics.General;
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
    /// A IGuiComponent is a rectangle, with measures Width/Height and position
    /// X/Y. 
    /// Every subtype is only allowed to draw inside this rectangle
    /// 
    /// Changed of the position or size is signaled by the corresponding event
    /// SizeChanged and PositionChanged.
    /// </remarks>
    public interface IGuiComponent
    {

        void Update();

        void HandleKeyInput(CommandKeys key);

        event EventHandler<ComponentSelectedEventArgs> ComponentSelected;

        event EventHandler<GraphicComponentSizeChangedEventArgs> PreferredSizeChanged;

        float PreferredHeight { get; }
        float PreferredWidth { get; }

        Rectangle ScissorArea { get; set; }
        Rectangle Area { get; set; }

        IGuiComponent Parent { get; set; }
        List<IGuiComponent> Children { get; }

        bool IsSelected { get; set; }
        bool IsSelectable { get; }
    }
}
