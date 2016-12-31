using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Renderers
{
    public interface ILabelRenderer : IRenderer<Label>
    {
        float GetPreferedWidth(Label label);
        float GetPreferedHeight(Label label);
    }
}