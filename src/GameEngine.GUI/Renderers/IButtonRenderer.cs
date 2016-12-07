using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Renderers
{
    public interface IButtonRenderer : IRenderer<Button>
    {
        float GetPreferedWidth(Button button);
        float GetPreferedHeight(Button button);
    }
}