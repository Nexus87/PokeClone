using GameEngine.Graphics;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI
{
    public interface ISkin
    {
        Color BackgroundColor { get; }
        void RegisterRenderers(IGameTypeRegistry registry, TextureProvider provider);
        void AddTextureConfigurations(TextureConfigurationBuilder builder);
    }
}