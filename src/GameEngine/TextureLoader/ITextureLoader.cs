using GameEngine.GUI.Configuration;
using GameEngine.GUI.Graphics;

namespace GameEngine.Core.TextureLoader
{
    public interface ITextureLoader
    {
        void AddFromConfiguration(TextureConfig textureConfig);
        void AddTextureProvider(ITextureProvider provider);
        IImageBox LoadTexture(string textureName);
    }
}
