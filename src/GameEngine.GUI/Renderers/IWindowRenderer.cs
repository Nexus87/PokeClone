using GameEngine.GUI.Renderers;

namespace GameEngine.GUI.Panels
{
    public interface IWindowRenderer : IRenderer<Window>
    {
        int LeftMargin { get; }
        int RightMargin { get; }
        int TopMargin { get; }
        int BottomMargin { get; }
    }
}