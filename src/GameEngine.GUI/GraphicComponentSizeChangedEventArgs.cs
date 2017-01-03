using System;

namespace GameEngine.GUI
{
    public class GraphicComponentSizeChangedEventArgs : EventArgs
    {
        public IGuiComponent Component { get; private set; }
        public float Width { get; private set; }
        public float Height { get; private set; }

        public GraphicComponentSizeChangedEventArgs(IGuiComponent component, float width, float height)
        {
            Component = component;
            Width = width;
            Height = height;
        }
    }
}