using System;

namespace GameEngine.GUI
{
    public class ComponentSelectedEventArgs : EventArgs
    {
        public IGraphicComponent Source { get; set; }
    }
}