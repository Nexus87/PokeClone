using System;

namespace GameEngine.Graphics
{
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