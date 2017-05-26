using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicImageBoxRenderer : ImageBoxRenderer
    {
        public override void Init(TextureProvider textureProvider)
        {
        }

        protected override void RenderComponent(ISpriteBatch spriteBatch, ImageBox component)
        {
            if (component.Image == null)
                return;

            RenderImage(spriteBatch, component.Image, component.Area);
        }
    }
}