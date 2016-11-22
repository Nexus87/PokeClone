using GameEngine.GUI.Renderers;

namespace GameEngine.GUI
{
    public interface ISkin
    {
        IButtonRenderer BuildButtonRenderer();

        float DefaultTextHeight { get; }
    }
}