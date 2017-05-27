using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;

namespace GameEngine.GUI.Renderers
{
    public interface IRenderer
    {
        void Render(ISpriteBatch spriteBatch, IGuiComponent component);
        void Init(TextureProvider textureProvider);
    }
}