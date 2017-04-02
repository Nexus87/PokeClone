using GameEngine.GUI.Renderers;

namespace PokemonShared.Gui.Renderer
{
    public abstract class HpTextRenderer : AbstractRenderer<HpText>
    {
        public abstract float GetPreferredWidth(HpText hpText);
        public abstract float GetPreferredHeight(HpText hpText);
    }
}