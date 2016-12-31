using GameEngine.GUI.General;
using GameEngine.GUI.Panels;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicWindowRenderer : WindowRenderer
    {
        private readonly ITexture2D _borderTexture;

        public ClassicWindowRenderer(ITexture2D borderTexture)
        {
            _borderTexture = borderTexture;
            LeftMargin = 50;
            RightMargin = 10;
            TopMargin = 100;
            BottomMargin = 75;
        }

        protected override void RenderComponent(ISpriteBatch spriteBatch, Window component)
        {
            RenderImage(spriteBatch, _borderTexture, component.Area);
        }
    }
}