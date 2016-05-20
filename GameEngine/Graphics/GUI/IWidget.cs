using System;

namespace GameEngine.Graphics.GUI
{
    /// <summary>
    /// A widget is a GrapicComponent, that can handle user input.
    /// </summary>
    /// <remarks>
    /// These classes are mainly used for building GUIs.
    /// </remarks>
    public interface IWidget : IInputHandler, IGraphicComponent
    {
        /// <summary>
        /// Event is triggered, when IsVisible changes
        /// </summary>
        event EventHandler<VisibilityChangedEventArgs> VisibilityChanged;

        /// <summary>
        /// This property shows, if the widget is visible.
        /// </summary>
        bool IsVisible { get; set; }
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
}