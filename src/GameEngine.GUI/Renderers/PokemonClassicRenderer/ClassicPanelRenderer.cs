using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using GameEngine.GUI.Panels;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicPanelRenderer : PanelRenderer
    {
        private ITexture2D _pixel;

        public override void Init(TextureProvider textureProvider)
        {
            _pixel = textureProvider.Pixel;
        }

        protected override void RenderComponent(ISpriteBatch spriteBatch, Panel component)
        {
            RenderImage(spriteBatch, _pixel, component.Area, component.BackgroundColor);
        }
    }
}