using GameEngine.GUI.Configuration;
using GameEngine.GUI.Renderers;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Core.TextureLoader
{
    public static class TextureProviderFactory
    {
        public static ITextureProvider GetProviderFromConfiguration(SpriteSheet spriteSheet, ContentManager contentManager)
        {
            return new SpriteSheetTextureProvider(spriteSheet, contentManager);
        }

        public static ITextureProvider GetProviderFromConfiguration(Texture texture, ContentManager contentManager, IGameTypeRegistry registry)
        {
            return new SingleTextureProvider(texture, registry.ResolveType<IImageBoxRenderer>(), contentManager);
        }
    }
}