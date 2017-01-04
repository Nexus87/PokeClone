using GameEngine.GUI.Renderers;

namespace GameEngine.Pokemon.Gui.Renderer
{
    public abstract class HpTextRenderer : AbstractRenderer<HpText>
    {
        public abstract float GetPreferredWidth(HpText hpText);
        public abstract float GetPreferredHeight(HpText hpText);
    }
}