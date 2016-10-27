using GameEngine.Configuration;
using GameEngine.Graphics;

namespace GameEngine.TextureLoader
{
    public interface ITextureLoader
    {
        void AddFromConfiguration(TextureConfig textureConfig);
        void AddTextureProvider(ITextureProvider provider);
        IImageBox LoadTexture(string textureName);
    }
}
