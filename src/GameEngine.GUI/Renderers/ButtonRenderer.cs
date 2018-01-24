using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Renderers
{
    public abstract class ButtonRenderer : AbstractRenderer<Button>
    {
        public abstract float GetPreferedWidth(Button button);
        public abstract float GetPreferedHeight(Button button);
    }
}