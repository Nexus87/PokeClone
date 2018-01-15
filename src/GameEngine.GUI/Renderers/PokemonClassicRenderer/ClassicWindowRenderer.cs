using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using GameEngine.GUI.Panels;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicWindowRenderer : WindowRenderer
    {
        private ITexture2D _borderTexture;

        public ClassicWindowRenderer()
        {
            LeftMargin = 50;
            RightMargin = 10;
            TopMargin = 100;
            BottomMargin = 75;
        }

        public override void Init(ITextureProvider textureProvider)
        {
            _borderTexture = textureProvider.GetTexture(ClassicSkin.Border);
        }

        protected override void RenderComponent(ISpriteBatch spriteBatch, Window component)
        {
            RenderImage(spriteBatch, _borderTexture, component.Area);
        }
    }
}