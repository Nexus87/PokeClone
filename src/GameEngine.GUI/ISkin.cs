using GameEngine.GUI.Renderers;

namespace GameEngine.GUI
{
    public interface ISkin
    {
        IButtonRenderer BuildButtonRenderer();

        IRenderer GetRendererForType<T>();
        float DefaultTextHeight { get; }
    }
}