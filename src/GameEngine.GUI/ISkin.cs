using GameEngine.Graphics.Textures;
using GameEngine.TypeRegistry;

namespace GameEngine.GUI
{
    public interface ISkin
    {
        void RegisterRenderers(IGameTypeRegistry registry, TextureProvider provider);
        void AddTextureConfigurations(TextureConfigurationBuilder builder);
    }
}