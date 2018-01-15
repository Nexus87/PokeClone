using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics.Textures
{
    public interface ITextureProvider
    {
        ITexture2D Pixel { get; }
        ITexture2D GetTexture(string textureIdentifier);
        void Init(GraphicsDevice device);
        ISpriteFont GetFont(string fontIdentifier);
    }
}