using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Panels;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassPanelRenderer : AbstractRenderer<Panel>, IPanelRenderer
    {
        private readonly ITexture2D _pixel;

        public ClassPanelRenderer(ITexture2D pixel)
        {
            _pixel = pixel;
        }

        public override void Render(ISpriteBatch spriteBatch, Panel component)
        {
            RenderImage(spriteBatch, _pixel, component.Area, component.BackgroundColor);
        }
    }
}