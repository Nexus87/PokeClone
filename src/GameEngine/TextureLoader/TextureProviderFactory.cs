using GameEngine.Configuration;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.TextureLoader
{
    public static class TextureProviderFactory
    {
        public static ITextureProvider GetProviderFromConfiguration(SpriteSheet spriteSheet, ContentManager contentManager)
        {
            return new SpriteSheetTextureProvider(spriteSheet, contentManager);
        }

        public static ITextureProvider GetProviderFromConfiguration(Texture texture, ContentManager contentManager)
        {
            return new SingleTextureProvider(texture, contentManager);
        }
    }
}