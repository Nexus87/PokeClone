using GameEngine.Graphics.General;
using GameEngine.GUI.Panels;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicPanelRenderer : PanelRenderer
    {
        private readonly ITexture2D _pixel;

        public ClassicPanelRenderer(ITexture2D pixel)
        {
            _pixel = pixel;
        }

        protected override void RenderComponent(ISpriteBatch spriteBatch, Panel component)
        {
            RenderImage(spriteBatch, _pixel, component.Area, component.BackgroundColor);
        }
    }
}