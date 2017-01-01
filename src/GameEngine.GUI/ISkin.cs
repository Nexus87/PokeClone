using GameEngine.Graphics;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.GUI
{
    public interface ISkin
    {
        Color BackgroundColor { get; }
        void RegisterRenderers(IGameTypeRegistry registry, TextureProvider provider);
        void AddTextureConfigurations(TextureConfigurationBuilder builder);
    }
}