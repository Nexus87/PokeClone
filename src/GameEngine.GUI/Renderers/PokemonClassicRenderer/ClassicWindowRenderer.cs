using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Panels;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicWindowRenderer : AbstractRenderer<Window>, IWindowRenderer
    {
        private readonly ITexture2D _borderTexture;

        public ClassicWindowRenderer(ITexture2D borderTexture)
        {
            _borderTexture = borderTexture;
        }

        public override void Render(ISpriteBatch spriteBatch, Window component)
        {
            RenderImage(spriteBatch, _borderTexture, component.Area);
        }

        public int LeftMargin { get; } = 100;
        public int RightMargin { get; } = 100;
        public int TopMargin { get; } = 100;
        public int BottomMargin { get; } = 100;
    }
}