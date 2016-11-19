using GameEngine.GUI.Renderers;

namespace GameEngine.GUI
{
    public interface ISkin
    {
        IButtonRenderer BuildButtonRenderer();

        T GetRendererForType<T>() where T : IRenderer;
        float DefaultTextHeight { get; }
    }
}