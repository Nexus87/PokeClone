using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Renderers
{
    public abstract class LabelRenderer : AbstractRenderer<Label>
    {
        public abstract float GetPreferedWidth(Label label);
        public abstract float GetPreferedHeight(Label label);
    }
}