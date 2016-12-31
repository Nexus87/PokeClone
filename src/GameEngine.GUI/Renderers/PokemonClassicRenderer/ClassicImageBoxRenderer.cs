using GameEngine.GUI.Controlls;
using GameEngine.GUI.General;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicImageBoxRenderer : AbstractRenderer<ImageBox>, IImageBoxRenderer
    {
        public override void Render(ISpriteBatch spriteBatch, ImageBox component)
        {
            if (component.Image == null)
                return;

            RenderImage(spriteBatch, component.Image, component.Area);
        }
    }
}