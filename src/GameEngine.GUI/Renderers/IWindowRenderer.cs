using GameEngine.GUI.Panels;

namespace GameEngine.GUI.Renderers
{
    public interface IWindowRenderer : IRenderer<Window>
    {
        int LeftMargin { get; }
        int RightMargin { get; }
        int TopMargin { get; }
        int BottomMargin { get; }
    }
}