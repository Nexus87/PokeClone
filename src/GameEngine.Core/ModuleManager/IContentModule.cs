using GameEngine.Graphics.Textures;

namespace GameEngine.Core.ModuleManager
{
    public interface IContentModule
    {
        void AddTextureConfigurations(TextureConfigurationBuilder builder);
    }
}
