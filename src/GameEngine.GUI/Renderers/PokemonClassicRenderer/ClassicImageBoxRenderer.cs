using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicImageBoxRenderer : ImageBoxRenderer
    {
        public override void Init(ITextureProvider textureProvider)
        {
        }

        protected override void RenderComponent(ISpriteBatch spriteBatch, ImageBox component)
        {
            if (component.Image == null)
                return;

            RenderImage(spriteBatch, component.Image, component.Area);
        }

        protected override void UpdateComponent(ImageBox component)
        {
        }
    }
}