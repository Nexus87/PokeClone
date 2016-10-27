using GameEngine.Graphics.NewGUI.Renderers;

namespace GameEngine.Graphics.NewGUI
{
    public interface ISkin
    {
        IButtonRenderer BuildButtonRenderer();

        IRenderer GetRendererForType<T>();
        float DefaultTextHeight { get; }
    }
}