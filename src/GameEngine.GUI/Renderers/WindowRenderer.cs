using GameEngine.GUI.Panels;

namespace GameEngine.GUI.Renderers
{
    public abstract class WindowRenderer : AbstractRenderer<Window>
    {
        public int LeftMargin { get; protected set; }
        public int RightMargin { get; protected set; }
        public int TopMargin { get; protected set; }
        public int BottomMargin { get; protected set; }
    }
}