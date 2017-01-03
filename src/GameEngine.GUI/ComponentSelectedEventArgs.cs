using System;

namespace GameEngine.GUI
{
    public class ComponentSelectedEventArgs : EventArgs
    {
        public IGuiComponent Source { get; set; }
    }
}